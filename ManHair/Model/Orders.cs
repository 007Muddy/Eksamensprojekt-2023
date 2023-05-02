using System;
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
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public List<Treatment> Treatments { get; set; } = new List<Treatment>();
        public double Price { get; set; }    
        public Orders(DateOnly date, TimeOnly time, List<Treatment> treatments, double price)
        {
            Date = date;
            Time = time;
            Treatments = treatments;
            Price = price;
        } 
    }
}
