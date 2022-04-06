using EAromaBLL.Repositories;
using EAromaDLL.Entities;
using EAromaWebUI.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        SqlRepo<CustomerService> repoCustomerService;
        //SqlRepo<ContactForm> repoContact;
        SqlRepo<FooterContact> repoFooterContact;
        public FooterViewComponent(SqlRepo<CustomerService> _repoCustomerService,  SqlRepo<FooterContact> _repoFooterContact)
        {
            repoCustomerService = _repoCustomerService;
            //repoContact = _repoContact;
            repoFooterContact = _repoFooterContact;
        }

        public IViewComponentResult Invoke()
        {
            FooterVM footerVM = new FooterVM
            {
                CustomerServices = repoCustomerService.GetAll().OrderBy(o => o.DisplayIndex),
                FooterContacts=repoFooterContact.GetAll()
            };


            return View(footerVM);
        }
    }
}

