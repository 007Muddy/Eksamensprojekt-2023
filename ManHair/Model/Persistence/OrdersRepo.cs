﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ManHair.Model;
using System.Configuration;

namespace ManHair.ViewModel.Repositories
{
    public class OrdersRepo
    {
        private string connectionString { get; } = ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString;
        public void Add(int ID, string date, string time, double price, int treatment)
        {

            try
            {
                
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Orders (CustomerID, date,time, price, treatment)"
                        + " VALUES(@ID, @date, @time, @price, @treatment)", sqlConnection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        command.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                        command.Parameters.Add("@time", SqlDbType.NVarChar).Value = time;
                        command.Parameters.Add("@price", SqlDbType.Float).Value = price;
                        command.Parameters.Add("@treatment", SqlDbType.Int).Value = treatment;
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
