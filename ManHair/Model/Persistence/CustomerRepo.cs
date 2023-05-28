using ManHair.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel.Repositories
{
    public class CustomerRepo
    {
        private string? connectionString;
        private List<Customer> CostumerList = new List<Customer>();
        //Reference to the connectionStrings made in App.Config and passing its name in the parameter

        public CustomerRepo() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            connectionString = config.GetConnectionString("MyDBConnection");
        }

        // Load all Costumers from database and polulating into CostumerList

        public void LoadAllCustomers()
        {
            try
            {
                //before we can access the database we have to connect to the database, here we use SqlConnection object and refer it to the connectionstring
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Now Connection is open and we can run a Query on the database
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT CostumerID, Name, PhoneNumber, Email, Password FROM Customer", connection))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();

                        while (dataReader.Read())
                        {
                            int ID = dataReader.GetInt32(0);
                            string Name = dataReader.GetString(1);
                            int phone = dataReader.GetInt32(2);
                            string Email = dataReader.GetString(3);
                            string Password = dataReader.GetString(4);
                            Customer costumer = new Customer(ID, Name, phone, Email, Password);
                            CostumerList.Add(costumer);
                        }
                       
                    }

                }

            }
            catch (SqlException ex)
            {

                throw new Exception("An error occured while trying to fetch data from the database");
            }
        }

        public List<Customer> GetCustomers() 
        {

            if (CostumerList.Count() == 0)
            {
                LoadAllCustomers();
            }
           return CostumerList;
        }

        public int GetID(string email)
        {
            int ID = 0;
            List<Customer> customers = GetCustomers();
            List<Customer> filteredCustomers = customers.Where(customer => customer.Email == email).ToList();
            foreach (Customer customerID in filteredCustomers)
            {
                ID = customerID.ID;
            }
            return ID;
        }
        public void AddCustomer(string name, int phone, string email, string password)
        {
          
            try
            {
                // A variable named "iterations" is initialized with the value of 10000.
                // This is the number of iterations that PBKDF2 will perform to generate the hash.
                int iterations = 10000;

                //A byte array named "salt" is created with a length of 16 bytes,
                //and it is filled with random bytes using the RNGCryptoServiceProvider class.
                byte[] salt; 
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                //New instans of the Rfc2898DeriveBytes Class

                //An instance of the Rfc2898DeriveBytes class is created, passing in the password, salt, and number of rounds as arguments.
                //This is the main class that performs the PBKDF2 algorithm.
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

                //The GetBytes method is called on the pbkdf2 instance to generate a byte array of the hashed password.
                //The length of the array is set to 20 bytes.
                byte[] hash = pbkdf2.GetBytes(20);
                //A byte array named "hashBytes" is created with a length of 36 bytes.
                byte[] hashBytes = new Byte[36];
                //The first 16 positions of the hashBytes array are filled with the
                //salt bytes using the Array.Copy method.
                Array.Copy(salt, 0, hashBytes, 0, 16);

                //The next 20 positions of the hashBytes array are filled with
                //the hashed password bytes using the Array.Copy method.
                Array.Copy(hash, 0, hashBytes, 16, 20);

                //It uses the Convert.ToBase64String method to convert the hashBytes array
                //into a base64 encoded string, which can be safely stored in the database
                string hashedPassword = Convert.ToBase64String(hashBytes);

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Customer (Name, PhoneNumber, Email, Password)" 
                        +" VALUES(@name, @phone, @email, @password)", sqlConnection))
                    {
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                        command.Parameters.Add("@phone", SqlDbType.Int).Value = phone;
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = hashedPassword;
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to add a new use to the DB" + e);
            }
        }
        public void RemoveCustomer(string email)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE Email = @email", sqlConnection))
                    {
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to remove a use to the DB" + e);
            }
        }
        public bool AuthenticateUser(Customer customer)
        {
            bool accepted = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT Email, Password FROM Customer", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //iterate through DB
                            while (reader.Read())
                            {
                                //Check if Email matches
                                if (customer.Email == reader["Email"].ToString())
                                {
                                    ////Decryption of password///
                                    //It then uses the Convert.FromBase64String method to convert the savedPasswordHash string
                                    //into a byte array called "hashBytes". This is because the hashed password is stored in a
                                    //Base64 encoded string format, and this method converts it back to its original binary format
                                    byte[] hashBytes = Convert.FromBase64String(reader["Password"].ToString());
                                    //Next, it creates a new byte array called "salt" with a length of 16 bytes,
                                    //which is the same length as the salt used in the encryption process
                                    byte[] salt = new byte[16];
                                    //It uses the Array.Copy method to copy the first 16 bytes of the
                                    //hashBytes array into the salt array. In this way we can separate the salt from the hash.
                                    Array.Copy(hashBytes, 0, salt, 0, 16);
                                    //It creates a new instance of the Rfc2898DeriveBytes class, passing in the user input password,
                                    //salt and number of rounds
                                    var pbkdf2 = new Rfc2898DeriveBytes(customer.Password, salt, 10000);
                                    //It then calls the GetBytes method to generate a byte array of the hashed password.
                                    byte[] hash = pbkdf2.GetBytes(20);

                                    accepted = true;
                                    //It then uses a for loop to iterate through the hashBytes array, starting at the 17th position (16th index),
                                    //which is where the actual hashed password begins, since the first 16 bytes are the salt
                                    for (int i = 0; i < 20; i++)
                                    {
                                        //It compares each byte of the hashBytes array to the corresponding byte in the hash array,
                                        //using an if statement. If any of the bytes do not match, accepted = false.
                                        if (hashBytes[i + 16] != hash[i])
                                        {
                                            accepted = false;
                                            break;
                                        }
                                        //If the for loop completes without throwing an exception,
                                        //it means that the user entered the correct password and it prints "password is correct!

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Der opstod en fejl med sammenligning af password" + e);
            }
            return accepted;
        }
        // Adding the customer login information to the Authentication Table
        public void AddAuthentication(string email, string password)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Authentication (Email, Password) VALUES(@email, @password)", sqlConnection))
                    {

                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to add a new use to the DB" + e);
            }
        }
        public void RemoveAuthentication()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("TRUNCATE TABLE Authentication", sqlConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to add a new use to the DB" + e);
            }
        }
        public string getEmail()
        {

            try
            {
                //before we can access the database we have to connect to the database, here we use SqlConnection object and refer it to the connectionstring
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Now Connection is open and we can run a Query on the database
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT Email FROM Authentication", connection))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            string email = dataReader.GetString(0);
                            return email;
                        }

                    }

                }

            }
            catch (SqlException ex)
            {

                throw new Exception("An error occured while trying to fetch data from the database");
            }
            return null;
        }
    }
}
