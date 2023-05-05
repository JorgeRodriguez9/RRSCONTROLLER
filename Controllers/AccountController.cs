using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RRS_Controller.DAL;
using RRS_Controller.Models;
using System.Security.Claims;

namespace RRS_Controller.Controllers
{
    public class AccountController : Controller
    {
        private readonly RSSCONTROLLERContext _context;

        public AccountController(RSSCONTROLLERContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(USER u)
        {
            try
            {
                using(SqlConnection conn = new())
                {
                    using (SqlCommand cmd = new("sp_validar_usuario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name_User", System.Data.SqlDbType.VarChar).Value = u.Name_User;
                        cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar).Value = u.Password;
                        conn.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["UserName"] != null && u.Name_User != null)
                            {
                                List<Claim> c = new List<Claim>();
                                {
                                    new Claim(ClaimTypes.NameIdentifier, u.Name_User);
                                }
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = u.MaintainActive;

                                if (!u.MaintainActive)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                else
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Incorrect credentials or unregistered account.";
                            }
                        }
                        conn.Close();
                    }
                    return View();
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}
