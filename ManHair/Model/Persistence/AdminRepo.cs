using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ManHair.Model.Persistence
{
    public class AdminRepo
    {
        private string? connectionString;
        
        private List<Admin> adminList { get; set; } = new();
        public AdminRepo() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            connectionString = config.GetConnectionString("MyDBConnection");
        }
        public List<Admin> RetrieveAdmins()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("SELECT Username,Password FROM Admin"))
                    {
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            string userName = reader.GetString(1);
                            string password = reader.GetString(2);

                            Admin admin = new Admin(userName, password);
                            adminList.Add(admin);
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("There was a problem fethcing admin from DB" + e);
            }

            return adminList;
        }
        public bool AdminAuthentication(Admin admin)
        {
            bool accepted = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand com = new SqlCommand("SELECT Username, Password FROM Admin ", connection))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())

                        {
                            while (reader.Read())

                            {
                                if (admin.UserName == reader["Username"].ToString() && admin.Password == reader["Password"].ToString())

                                {
                                    accepted = true;

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
