using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Diagnostics;

namespace RRSCONTROLLER.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;

        private readonly RSSCONTROLLERContext _context;

        public ManagerController(RSSCONTROLLERContext context, ILogger<ManagerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult HomeManagerPAE()
        {
            return View();
        }
        // GET: AdminPaeController/RegisterInstitution

        [Authorize(Roles = "Manager")]
        public IActionResult RegisterInstitution()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> RegisterInstitution(string name, string adress, string numberPhone, string email)
        {

            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var manager = _context.MANAGER_PAEs.FirstOrDefault(p => p.Id_User == user).ID;

            if (name != null && adress != null && numberPhone != null && email != null)
            {
                var institution = new INSTITUTION
                {
                    Name = name,
                    Adress = adress,
                    Phone_Number = long.Parse(numberPhone),
                    Email = email,
                    Id_Manager = manager
                };
                _context.INSTITUTIONS.Add(institution);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "La institución se ha registrado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Occurrio un error al momento de registrar la institucion.";
            }

            return RedirectToAction("RegisterInstitution");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Reports()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult InstitutionReport()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
