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

        // GET: ManagerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult HomeManagerPAE()
        {
            return View();
        }
        // GET: AdminPaeController/RegisterInstitution
        public IActionResult RegisterInstitution()
        {
            return View();
        }

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
                TempData["SuccessMessage"] = "The product was registered successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while registering the product.";
            }

            return RedirectToAction("RegisterInstitution");
        }

        public IActionResult Reports()
        {
            return View();
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
