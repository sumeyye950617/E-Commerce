using EAromaDLL.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewModels
{
    public class FooterVM
    {
        public IEnumerable<CustomerService> CustomerServices { get; set; }
        public IEnumerable<FooterContact> FooterContacts { get; set; }
        public IEnumerable<ContactForm> Contacts { get; set; }
    }
}
