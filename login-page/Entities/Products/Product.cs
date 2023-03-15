using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Entities.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public double ArrivalPrice { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
