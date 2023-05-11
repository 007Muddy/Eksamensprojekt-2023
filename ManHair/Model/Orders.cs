﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string OrderDate { get; set; }
        public string Time { get; set; }

        public double Price { get; set; }
        public string Treatment { get; set; }

        public Orders(int orderID, string orderDate, string time, double price, string treatment, int customerID)
        {
            OrderID = orderID;
            CustomerID = customerID;
            OrderDate = orderDate;
            Time = time;
            Treatment = treatment;
            Price = price;
        }
    }
}
