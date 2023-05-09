using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ManHair.ViewModel.Repositories
{
    public class OrdersRepo
    {
       private List<Orders> ordersList = new List<Orders>();
        private string connectionstring { get; } = ConfigurationManager.ConnectionStrings["Databasestring"].ConnectionString;

        public List<Orders> RetrieveOrders()
        {
        
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionstring))
                {
                    sqlCon.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Orders", sqlCon))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            int orderID = dataReader.GetInt32(0);
                            string orderDate = dataReader.GetString(1);
                            string time = dataReader.GetString(2);
                            double price = dataReader.GetDouble(3);

                            string treatment = dataReader.GetString(4); 
                            int customerID = dataReader.GetInt32(5);

                            Orders orders = new Orders(orderID, orderDate, time, price,  treatment,customerID);
                            ordersList.Add(orders);
                          
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve orders", ex);
            }
            return ordersList;
        }
        public List<Orders> GetOrders() 
        {

            if (ordersList.Count == 0)
            {
                RetrieveOrders();
            }
        return ordersList;
        }
    }
}
