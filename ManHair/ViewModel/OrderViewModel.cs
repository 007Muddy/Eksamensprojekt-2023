using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public string Treatment { get; set; }
        public double Price { get; set; }

        public OrderViewModel(Order order)

        {
            OrderID = order.OrderID;
            CustomerID = order.CustomerID;
            Date = order.Date;
            Time = order.Time;
            Treatment = order.Treatment;
            Price = order.Price;


        }

    }
}
