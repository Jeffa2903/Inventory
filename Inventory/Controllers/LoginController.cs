using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {

        private AppDbContext db = new AppDbContext();
        // GET: Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(login model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Logins.FirstOrDefault(s => s.userName == model.userName);
                if (user != null)
                {
                    string hashedPassword = HashPassword(model.password);
                    if (user.password == hashedPassword)
                    {
                        // Password benar, lakukan tindakan setelah login berhasil
                        // Contoh: Set session atau cookie untuk pengguna yang sudah login
                        Session["Username"] = user.userName;
                        Session["Role"] = user.role;
                        string username = Session["Username"].ToString();
                        string role = Session["Role"].ToString();
                        ViewBag.username = username;
                        ViewBag.role = role;
                        return RedirectToAction("Index", "Home"); // Redirect ke halaman setelah login berhasil
                    }

                    FormsAuthentication.SetAuthCookie(user.userName, false);

                    // Handle role-based authentication here
                    // Contoh:
                    //if (user.role == "admin")
                    //{
                    //    return RedirectToAction("AdminDashboard", "Admin");
                    //}
                    //else if (user.role == "user")
                    //{
                    //    return RedirectToAction("UserDashboard", "User");
                    //}
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username atau password salah.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}