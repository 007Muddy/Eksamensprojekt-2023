using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Markup;

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
                            int bitwise = int.Parse(dataReader.GetString(4));
                            List<string> treatment = GetTreatmentTypesFromDB(bitwise);
                            int customerID = dataReader.GetInt32(5);
                            // string.Join() in the RetrieveOrders() method is to concatenate the list of treatment type strings (treatment) we have used \ to look better on GUI.
                            // This is done to create a more human-readable representation of the treatment types when displayed in the ListView.
                            string treatmentString = string.Join(" \n ", treatment);

                            Orders orders = new Orders(orderID, orderDate, time, price, treatmentString, customerID);
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

        // this method takes an integer bitwise value as input and returns a
        // list of strings representing the corresponding treatment types.
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
