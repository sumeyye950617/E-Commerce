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
    public class CategoryController : Controller
    {
        SqlRepo<Category> repoCategory;
        public CategoryController(SqlRepo<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }

        [Route("/admin/kategori")]
        public IActionResult Index()
        {
            return View(repoCategory.GetAll().OrderByDescending(o => o.ID));
        }

        [Route("/admin/kategori/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/kategori/yeni"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult New(Category model)
        {
            repoCategory.Add(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/kategori/guncelle/{id}")]
        public IActionResult Update(int id)
        {
            return View(repoCategory.GetAll().FirstOrDefault(x => x.ID == id));
        }

        [Route("/admin/kategori/guncelle/{id}"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Category model)
        {
            repoCategory.Update(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/kategori/sil/{id}")]
        public IActionResult Delete(int id)
        {
            repoCategory.Delete(repoCategory.GetAll().FirstOrDefault(x => x.ID == id));
            return RedirectToAction("Index");
        }
    }
}

