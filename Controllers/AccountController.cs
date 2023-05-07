using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Security.Claims;


namespace RRSCONTROLLER.Controllers
{
    public class AccountController : Controller
    {
        private readonly RSSCONTROLLERContext _context;

        public static string Var;

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
                {

                    string userName = HttpContext.Session.GetString("UserName") ?? Var;

                    var role = _context.USERS.FirstOrDefault(p => p.Name_User == userName).Id_Role;

                    if (role == 1)
                    {
                        return RedirectToAction("HomeManagerPAE", "Manager");
                    }
                    if (role == 2)
                    {
                        return RedirectToAction("HomeAdministrator", "AdminPae");
                    }
                    if (role == 3)
                    {
                        return RedirectToAction("HomeNutritionistPae", "NutritionistPae");
                    }

                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(USER u)
        {
            try
            {
                using(SqlConnection conn = new("Server= CARLOS_RAMOS\\SQLEXPRESS;Database=RRSCONTROLLER;Database=RRSCONTROLLER;TrustServerCertificate=True;Integrated Security=True"))
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
                            if (dr["Name_User"] != null && u.Name_User != null)
                            {
                                List<Claim> c = new List<Claim>();
                                {
                                    c.Add(new Claim(ClaimTypes.Name, u.Name_User));
                                    new Claim(ClaimTypes.NameIdentifier, u.Name_User);
                                }
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = u.MaintainActive;

                                if (!u.MaintainActive)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5);
                                else
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(50);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

                                HttpContext.Session.SetString("UserName", u.Name_User);

                                var a = _context.USERS.FirstOrDefault(p => p.Name_User == u.Name_User).Id_Role;

                                Var = u.Name_User;

                                if (a == 1)
                                {
                                    return RedirectToAction("HomeManagerPAE", "Manager");
                                }
                                if (a == 2)
                                {
                                    return RedirectToAction("HomeAdministrator", "AdminPae");
                                }
                                if (a == 3)
                                {
                                    return RedirectToAction("HomeNutritionistPae", "NutritionistPae");
                                }

                               
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
