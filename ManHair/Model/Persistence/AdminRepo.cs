using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model.Persistence
{
    public class AdminRepo
    {
        private string connectionString { get; } = ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString;
        private List<Admin> adminList { get; set; } = new();
        public List<Admin> RetrievAdmins()
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

    }
}
