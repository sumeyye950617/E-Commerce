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
    public class SlideController : Controller
    {
        SqlRepo<Banner> repoSlide;
        public SlideController(SqlRepo<Banner> _repoBanner)
        {
            repoSlide = _repoBanner;
        }

        [Route("/admin/slayt")]
        public IActionResult Index()
        {
            return View(repoSlide.GetAll().OrderByDescending(o => o.ID));
        }


        [Route("/admin/slayt/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/slayt/yeni"),HttpPost,ValidateAntiForgeryToken]
        public IActionResult New(Banner model)
        {
            if (Request.Form.Files.Any())//en az bir dosya geliyorsa
            {
                string slidePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide");
                //   C:\Users\Developer\source\repos\ECommerce\ECommerce.WebUI\wwwroot\slide\abc\xyz
                if (!Directory.Exists(slidePath)) Directory.CreateDirectory(slidePath);
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", Request.Form.Files["Picture"].FileName), FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/img/slide/" + Request.Form.Files["Picture"].FileName;
            }
            repoSlide.Add(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/slayt/guncelle/{id}")]
        public IActionResult Update(int id)
        {
            return View(repoSlide.GetAll().FirstOrDefault(x => x.ID == id));
        }

        [Route("/admin/slayt/guncelle/{id}"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Banner model)
        {
            if (Request.Form.Files.Any())//en az bir dosya geliyorsa
            {
                string slidePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide");
                //   C:\Users\Developer\source\repos\ECommerce\ECommerce.WebUI\wwwroot\slide\abc\xyz
                if (!Directory.Exists(slidePath)) Directory.CreateDirectory(slidePath);
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", Request.Form.Files["Picture"].FileName), FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/img/slide/" + Request.Form.Files["Picture"].FileName;
            }
            repoSlide.Update(model);
            return RedirectToAction("Index");
        }

        [Route("/admin/slayt/sil/{id}")]
        public IActionResult Delete(int id)
        {
            repoSlide.Delete(repoSlide.GetAll().FirstOrDefault(x => x.ID == id));
            return RedirectToAction("Index");
        }
    }
    }

