using ManHair.Model;
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
    public class CostumerRepo
    {
        
        private List<Customer> CostumerList = new List<Customer>();
        //Reference to the connectionStrings made in App.Config and passing its name in the parameter
        private string connectionString { get; } = ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString;

        // Load all Costumers from database and polulating into CostumerList

        public void loadAllCustomers()
        {
            try
            {
                //before we can access the database we have to connect to the database, here we use SqlConnection object and refer it to the connectionstring
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Now Connection is open and we can run a Query on the database
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection))
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

        public List<Customer> getCostumers() 
        {

            if (CostumerList.Count == 0)
            {
                loadAllCustomers();
            }
           return CostumerList;
        }

        public void Add(string name, int phone, string email, string password)
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

    }
}
