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
        public string Date { get; set; }
        public string Time { get; set; }
        public List<Treatment> Treatments { get; set; } = new List<Treatment>();
        public double Price { get; set; }

        public OrdersViewModel(Orders order)
        {
            Date = order.Date;
            Time = order.Time;  
            Price = order.Price;
            this.Treatments = order.Treatments;
        }
    }
}
