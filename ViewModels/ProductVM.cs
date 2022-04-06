using EAromaDLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }
    }
}
