using EAromaDLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewModels
{
    public class IndexVM
    {
  
        public  IEnumerable<Banner> Slides { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Product> TrendProducts { get; set; }
        public IEnumerable<Product> BestSellerProducts { get; set; }
    }
}
