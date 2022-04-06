using EAromaDLL.Entities;
using EAromaWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewModels
{
    public class CheckOutVM
    {
        public IEnumerable<Cart> Carts { get; set; }
        public Order Order { get; set; }
    }
}
