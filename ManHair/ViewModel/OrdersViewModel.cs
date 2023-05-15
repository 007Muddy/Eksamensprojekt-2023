using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class OrdersViewModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public string Treatment { get; set; }
        public double Price { get; set; }

        public OrdersViewModel(Orders orders)

        {
            OrderID = orders.OrderID;
            CustomerID = orders.CustomerID;
            Date = orders.Date;
            Time = orders.Time;
            Treatment = orders.Treatment;
            Price = orders.Price;


        }

    }
}
