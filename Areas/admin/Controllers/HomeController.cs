using EAromaBLL.Repositories;
using EAromaDLL.Entities;
using EAromaWebUI.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EAromaWebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class HomeController : Controller
    {
        SqlRepo<Admin> repoAdmin;
        public HomeController(SqlRepo<Admin> _repoAdmin)
        {
            repoAdmin = _repoAdmin;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/giris"), AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("/admin/giris"), AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            Admin admin = repoAdmin.GetAll(x => x.UserName == username && x.Password == GeneralTool.getMD5(password)).FirstOrDefault() ?? null;
            if (admin == null) TempData["result"] = "Geçersiz Kullanıcı Adı veya Şifre";
            else
            {

                ClaimsIdentity claimsIdentity = new ClaimsIdentity("Divisima");
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, admin.NameSurname));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()));
                //claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, admin.Email....)));
                //claimsIdentity.AddClaim(new Claim("Iskonto", admin.Iskonto));

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true });

                if (string.IsNullOrEmpty(ReturnUrl)) return Redirect("/admin");
                else return Redirect(ReturnUrl);
            }
            return View();
        }

        [Route("/admin/cikis")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }

}

