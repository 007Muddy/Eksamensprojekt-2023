﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public double Price { get; set; }
        public string Treatment { get; set; }

        public Order(int orderID, string date, string time, double price, string treatment, int customerID)
        {
            OrderID = orderID;
            CustomerID = customerID;
            Date = date;
            Time = time;
            Treatment = treatment;
            Price = price;
        }
    }
}
