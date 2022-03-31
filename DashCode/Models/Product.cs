using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashCode.Models
{
    public class Product
    {
        public Product(string name, string fullName, int price, string image)
        {
            Name = name;
            FullName = fullName;
            Price = price;
            Image = image;
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
