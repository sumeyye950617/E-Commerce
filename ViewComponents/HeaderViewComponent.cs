using EAromaBLL.Repositories;
using EAromaDLL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        SqlRepo<Category> repoCategory;
        public HeaderViewComponent(SqlRepo<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }

        public IViewComponentResult Invoke()
        {
            return View(repoCategory.GetAll().OrderBy(o => o.Name));
        }
    }
}
