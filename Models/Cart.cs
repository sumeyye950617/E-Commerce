using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
