using EAromaBLL.Repositories;
using EAromaDLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class BrandController : Controller
    {
        SqlRepo<Brand> repoBrand;
        public BrandController(SqlRepo<Brand> _repoBrand)
        {
            repoBrand = _repoBrand;
        }

        [Route("/admin/marka")]
        public IActionResult Index()
        {
            return View(repoBrand.GetAll().OrderByDescending(o => o.ID));
        }

        [Route("/admin/marka/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/marka/yeni"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult New(Brand model)
        {
            repoBrand.Add(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/marka/guncelle/{id}")]
        public IActionResult Update(int id)
        {
            return View(repoBrand.GetAll().FirstOrDefault(x => x.ID == id));
        }

        [Route("/admin/marka/guncelle/{id}"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Brand model)
        {
            repoBrand.Update(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/marka/sil/{id}")]
        public IActionResult Delete(int id)
        {
            repoBrand.Delete(repoBrand.GetAll().FirstOrDefault(x => x.ID == id));
            return RedirectToAction("Index");
        }
    }
}

