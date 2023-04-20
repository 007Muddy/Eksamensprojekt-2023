using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManHair.ViewModel.Repositories
{
    public class AuthenticationRepo
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString;
        public Customer customer { get; set; }  

       public bool AuthenticateUser(Customer customer)
        {
            bool accepted = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new  SqlCommand("SELECT Email, Password FROM Customer",connection))
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

                throw new Exception("Der opstod en fejl med sammenligning af password");
            }
            return accepted;
        }


    }
}
