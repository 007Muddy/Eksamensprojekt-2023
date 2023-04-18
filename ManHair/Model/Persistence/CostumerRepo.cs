using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel.Repositories
{
    public class CostumerRepo
    {
        private List<Costumer> CostumerList = new List<Costumer>(); 

        //Reference to the connectionStrings made in App.Config and passing its name in the parameter
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        // Load all Costumers from database and polulating into CostumerList
        public CostumerRepo()
        {
            try
            {
                //before we can access the database we have to connect to the database, here we use SqlConnection object and refer it to the connectionstring
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Now Connection is open and we can run a Query on the database
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM ManHair_Costumer", connection))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();

                        int CostumerID = dataReader.GetInt32(0);
                        string Name = dataReader.GetString(1);
                        string phone = dataReader.GetString(2);
                        string Email = dataReader.GetString(3);
                        string Password = dataReader.GetString(4);
                        Costumer costumer = new Costumer(CostumerID, Name, phone, Email, Password);
                        CostumerList.Add(costumer);
                    }

                }

            }
            catch (SqlException ex)
            {

                throw new Exception("An error occured while trying to fetch data from the database");
            }
        }

        public bool CreateNewUser(string name, string phone, string email, string password)
        {
            bool addSucces = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TABLE Salon_Costumer (Name, Phone, Email, Password)" 
                        +" VALUES(@name, @phone, @email, @password)", sqlConnection))
                    {
                        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                        command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                        command.ExecuteNonQuery();

                        addSucces = true;
                        return addSucces;
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something sent wrong when trying to add a new use to the DB");
            }
        }

    }
}
