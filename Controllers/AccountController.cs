using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;

namespace RRSCONTROLLER.Controllers
{
    public class AccountController : Controller
    {
        private readonly RSSCONTROLLERContext _context;

        public static string Var;

        private readonly IConfiguration _configuration;

        public AccountController(RSSCONTROLLERContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Method that serves to maintain a role on the page without leaving it unless the user wants it
        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {

                    string userName = HttpContext.Session.GetString("UserName") ?? Var;

                    if(userName != null)
                    {
                        var role = _context.USERS.FirstOrDefault(p => p.Name_User == userName).Id_Role;

                        if (role == (int)DiccionaryB.A)
                        {
                            return RedirectToAction("HomeManagerPAE", "Manager");
                        }
                        if (role == (int)DiccionaryB.B)
                        {
                            return RedirectToAction("HomeAdministrator", "AdminPae");
                        }
                        if (role == (int)DiccionaryB.C)
                        {
                            return RedirectToAction("HomeNutritionistPae", "NutritionistPae");
                        }
                        if (role == (int)DiccionaryB.D)
                        {
                            return RedirectToAction("HomeNutritionistINST", "NutritionistInst");
                        }
                        if (role == (int)DiccionaryB.E)
                        {
                            return RedirectToAction("HomeSecretary", "Secretary");
                        }
                    }
                    else
                    {
                        HttpContext.Session.Clear();
                        return View();
                    }
                }
            }
            return View();
        }

        //Method that validates password and user in the DB and also find out what type of role is the user who registered
        [HttpPost]
        public async Task<IActionResult> Login(USER u)
        {
            try
            {

                string conection = _configuration.GetConnectionString("Conn");

                using (SqlConnection conn = new(conection))
                {
                    using (SqlCommand cmd = new("sp_validate_user", conn))
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

                                var a = _context.USERS.FirstOrDefault(p => p.Name_User == u.Name_User).Id_Role;

                                var b = _context.ROLES.FirstOrDefault(p => p.ID == a).Name_Role;

                                c.Add(new Claim(ClaimTypes.Role, b));

                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = u.MaintainActive;

                                if (!u.MaintainActive)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(((int)DiccionaryB.E * (int)DiccionaryB.D));
                                else
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(((int)DiccionaryB.E * (int)DiccionaryB.E) * (int)DiccionaryB.B);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

                                HttpContext.Session.SetString("UserName", u.Name_User);

                                Var = u.Name_User;

                                if (a == (int)DiccionaryB.A)
                                {
                                    return RedirectToAction("HomeManagerPAE", "Manager");
                                }
                                if (a == (int)DiccionaryB.B)
                                {
                                    return RedirectToAction("HomeAdministrator", "AdminPae");
                                }
                                if (a == (int)DiccionaryB.C)
                                {
                                    return RedirectToAction("HomeNutritionistPae", "NutritionistPae");
                                }
                                if (a == (int)DiccionaryB.D)
                                {
                                    return RedirectToAction("HomeNutritionistINST", "NutritionistInst");
                                }
                                if (a == (int)DiccionaryB.E)
                                {
                                    return RedirectToAction("HomeSecretary", "Secretary");
                                }

                            }
                            else
                            {
                                ViewBag.Error = "Datos Incorrectos.";
                            }
                        }
                        conn.Close();
                    }
                    ViewBag.Error = "Datos Incorrectos.";
                    return View();
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverPassword(string user)
        {
            try
            {
                var idUser = _context.USERS.FirstOrDefault(u => u.Name_User == user).ID;
                var a = "";
                string ad = _context.ADMIN_PAEs.FirstOrDefault(u => u.Id_User == idUser)?.Email ?? null;
                string ma = _context.MANAGER_PAEs.FirstOrDefault(u => u.Id_User == idUser)?.Email ?? null;
                string nut1 = _context.NUTRITIONITS_INTSs.FirstOrDefault(u => u.Id_User == idUser)?.Email ?? null;
                string nut2 = _context.NUTRITIONITS_PAEs.FirstOrDefault(u => u.Id_User == idUser)?.Email ?? null;
                string sec = _context.SECRETARY_INTSs.FirstOrDefault(u => u.Id_User == idUser)?.Email ?? null;

                if (ad != null)
                {
                    a = _context.ADMIN_PAEs.FirstOrDefault(u => u.Id_User == idUser).Email;
                }
                if (ma != null)
                {
                    a = _context.MANAGER_PAEs.FirstOrDefault(u => u.Id_User == idUser).Email;
                }
                if (nut1 != null)
                {
                    a = _context.NUTRITIONITS_INTSs.FirstOrDefault(u => u.Id_User == idUser).Email;
                }
                if (nut2 != null)
                {
                    a = _context.NUTRITIONITS_PAEs.FirstOrDefault(u => u.Id_User == idUser).Email;
                }
                if (sec != null)
                {
                    a = _context.SECRETARY_INTSs.FirstOrDefault(u => u.Id_User == idUser).Email;
                }

                string conection = _configuration.GetConnectionString("Conn");
                SqlConnection con = new SqlConnection(conection);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from USERS where Name_User = '" + user + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    dr.Read();
                    string nUser = dr["Name_User"].ToString();
                    string pw = dr["Password"].ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("El siguiente usuario a solicitado recuperar su contraseña:");
                    sb.AppendLine(nUser);
                    sb.AppendLine("Informacion: ");
                    sb.AppendLine("Correo: " + a);
                    sb.AppendLine("Contraseña: " + pw);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("soporterrscontroller274@gmail.com", "ygwwplxqhfhrltqc");//(Correo y Password carlitos)
                    MailMessage msg = new MailMessage();
                    msg.To.Add("soporterrscontroller274@gmail.com");
                    msg.From = new MailAddress("shopping card..<soporterrscontroller274@gmail.com>");//<Carlitos en el video dice company que creo que es gmail pero no estoy seguro y emailId osea el mismo correo de linea 47 dentro de los dimantes minuto 19:20 del video>
                    msg.Subject = "Solicitud Recuperacion de Contraseña";
                    msg.Body = sb.ToString();
                    client.Send(msg);
                    ViewBag.msg = "Solicitud de Recuperacion Enviada";
                    return View();

                }
                else
                {
                    ViewBag.msg = "Usuario Invalido";
                    return View();
                }
            }
            catch (Exception ex)
            {

                ViewBag.msg = "ERROR:" + ex.Message.ToString();
                return View();

            }
        }

        //Method that provides a way to cache the HTTP response generated by the "Error" action method for a specified period of time
        [ResponseCache(Duration = (int)DiccionaryB.X, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}
