using EAromaBLL.Repositories;
using EAromaDLL.Entities;
using EAromaWebUI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAromaWebUI.Areas.admin.Controllers
{

    [Area("admin"), Authorize]
    public class ProductController : Controller
    {
        SqlRepo<Product> repoProduct;
        SqlRepo<Brand> repoBrand;
        SqlRepo<Category> repoCategory;
        public ProductController(SqlRepo<Product> _repoProduct, SqlRepo<Brand> _repoBrand, SqlRepo<Category> _repoCategory)
        {
            repoProduct = _repoProduct;
            repoBrand = _repoBrand;
            repoCategory = _repoCategory;
        }

        [Route("/admin/urun")]
        public IActionResult Index()
        {
            return View(repoProduct.GetAll().Include(i => i.Brand).Include(i => i.ProductPictures).Include(i => i.ProductCategories).ThenInclude(th => th.Category).OrderByDescending(o => o.ID));
        }

        [Route("/admin/urun/yeni")]
        public IActionResult New()
        {
            //ViewBag.Markalar =new SelectList(repoBrand.GetAll().OrderBy(o => o.Name),"ID","Name");
            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                Brands = repoBrand.GetAll().OrderBy(o => o.Name),
                Categories = repoCategory.GetAll().OrderBy(o => o.Name)
            };
            return View(productVM);
        }

        //[Route("/admin/urun/yeni"), HttpPost, ValidateAntiForgeryToken]
        //public IActionResult New(Product model)
        //{
        //    repoProduct.Add(model.Product);
        //    return RedirectToAction("Index");
        //}

        [Route("/admin/urun/guncelle/{id}")]
        public IActionResult Update(int id)
        {
            ProductVM productVM = new ProductVM
            {
                Product = repoProduct.GetAll().FirstOrDefault(x => x.ID == id),
                Brands = repoBrand.GetAll().OrderBy(o => o.Name),
                Categories = repoCategory.GetAll().OrderBy(o => o.Name)
            };
            return View(productVM);
        }

        [Route("/admin/urun/guncelle/{id}"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Product model)
        {
            repoProduct.Update(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/urun/sil/{id}")]
        public IActionResult Delete(int id)
        {
            repoProduct.Delete(repoProduct.GetAll().FirstOrDefault(x => x.ID == id));
            return RedirectToAction("Index");
        }
    }
}
