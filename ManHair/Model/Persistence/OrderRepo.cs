using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ManHair.Model;
using System.Configuration;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;

namespace ManHair.ViewModel.Repositories
{
    public class OrderRepo
    {
        private string? connectionString;
        private List<Order> ordersList = new List<Order>();
        private List<Order> filteredOrders = new List<Order>();

        public OrderRepo() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            connectionString = config.GetConnectionString("MyDBConnection");
        }

        public List<Order> RetrieveOrders()
        {
            try
            {
                if (ordersList.Count() > 0)
                {
                    ordersList.Clear();
                }
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand command = new SqlCommand("SELECT OrdersID,Date,Time,Price,Treatment,CostumerID FROM Orders", sqlCon))
                    {
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            int orderID = dataReader.GetInt32(0);
                            string orderDate = dataReader.GetString(1);
                            string time = dataReader.GetString(2);
                            double price = dataReader.GetDouble(3);
                            int bitwise = int.Parse(dataReader.GetString(4));
                            List<string> treatment = GetTreatmentTypesFromDB(bitwise);
                            int customerID = dataReader.GetInt32(5);
                            // string.Join() in the RetrieveOrders() method is to concatenate the list of treatment type strings (treatment) we have used \ to look better on GUI.
                            // This is done to create a more human-readable representation of the treatment types when displayed in the ListView.
                            string treatmentString = string.Join(" \n ", treatment);

                            Order orders = new Order(orderID, orderDate, time, price, treatmentString, customerID);
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

        public List<string> GetTreatmentTypesFromDB(int bitwiseValue)
        {
            List<string> treatmentTypes = new List<string>();
            //int bitCast = int.Parse(bitwiseValue);

            foreach (Treatment.TreatmentType type in Enum.GetValues(typeof(Treatment.TreatmentType)))
            {
                if ((bitwiseValue & (int)type) == (int)type)
                {
                    treatmentTypes.Add(type.ToString());
                }
            }
            return treatmentTypes;
        }

        public List<Order> GetCustomerOrders(int id)
        {
                List<Order> orders = RetrieveOrders();
                filteredOrders = orders.Where(orders => orders.CustomerID == id)
                        .OrderBy(orders => orders.Date)
                       .ThenBy(orders => orders.Time).ToList();
            return filteredOrders;
        }

        public void AddOrder(int id, string date, string time, double price, int treatment)
        {
            try
            {  
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Orders (Date, Time, Price, Treatment, CostumerID)"
                        + " VALUES(@date, @time, @price, @treatment, @ID)", sqlConnection))
                    {
                        
                        command.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
                        command.Parameters.Add("@time", SqlDbType.NVarChar).Value = time;
                        command.Parameters.Add("@price", SqlDbType.Float).Value = price;
                        command.Parameters.Add("@treatment", SqlDbType.Int).Value = treatment;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException e)
            {

                        throw new Exception("Something went wrong when trying to add a new use to the DB" + e);
            }
        }
        public void RemoveOrder(int orderID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand command = new SqlCommand("DELETE FROM Orders" + " WHERE OrdersID = @ordersID", sqlConnection))
                    {
                        command.Parameters.Add("@ordersID", SqlDbType.Int).Value = orderID;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Something went wrong when trying to remove a use to the DB" + e);
            }
        }
    }
}
