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
    public class BannerController : Controller
    {
        SqlRepo<Banner> repoBanner;
        public BannerController(SqlRepo<Banner> _repoBanner)
        {
            repoBanner = _repoBanner;
        }

        [Route("/admin/banner")]
        public IActionResult Index()
        {
            return View(repoBanner.GetAll().OrderByDescending(o => o.ID));
        }

        [Route("/admin/banner/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/banner/yeni"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult New(Banner model)
        {
            if (Request.Form.Files.Any())//en az bir dosya geliyorsa
            {
                string BannerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Banner");
                //   C:\Users\Developer\source\repos\ECommerce\ECommerce.WebUI\wwwroot\Banner\abc\xyz
                if (!Directory.Exists(BannerPath)) Directory.CreateDirectory(BannerPath);
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Banner", Request.Form.Files["Picture"].FileName), FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/img/Banner/" + Request.Form.Files["Picture"].FileName;
            }
            repoBanner.Add(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/slayt/guncelle/{id}")]
        public IActionResult Update(int id)
        {
            return View(repoBanner.GetAll().FirstOrDefault(x => x.ID == id));
        }

        [Route("/admin/slayt/guncelle/{id}"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Banner model)
        {
            if (Request.Form.Files.Any())//en az bir dosya geliyorsa
            {
                string BannerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Banner");
                //   C:\Users\Developer\source\repos\ECommerce\ECommerce.WebUI\wwwroot\Banner\abc\xyz
                if (!Directory.Exists(BannerPath)) Directory.CreateDirectory(BannerPath);
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Banner", Request.Form.Files["Picture"].FileName), FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/img/Banner/" + Request.Form.Files["Picture"].FileName;
            }
            repoBanner.Update(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/slayt/sil/{id}")]
        public IActionResult Delete(int id)
        {
            repoBanner.Delete(repoBanner.GetAll().FirstOrDefault(x => x.ID == id));
            return RedirectToAction("Index");
        }
    }
}
