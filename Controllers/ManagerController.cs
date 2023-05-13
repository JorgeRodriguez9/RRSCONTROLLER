using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;

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
        //Method used to exit the user from the RRSCONTROLLER application
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        //Method used to return the home view of the manager
        [Authorize(Roles = "Gerente")]
        public IActionResult HomeManagerPAE()
        {
            return View();
        }

        //Method used to return the RegisterInstitution view
        [Authorize(Roles = "Gerente")]
        public IActionResult RegisterInstitution()
        {
            return View();
        }

        //Method used to save the institution in the database according to the data registered in the RegisterInstitution view
        [Authorize(Roles = "Gerente")]
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

        //Method used to generate reports for the manager made up of the evaluation and delivery
        [Authorize(Roles = "Gerente")]
        public IActionResult Reports()
        {
            var eva = _context.EVALUATIONS.ToList();
            var secre = _context.SECRETARY_INTSs.ToList();
            var print = new List<string>();

            var fList = eva.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Id_Secretary_Inst.ToString(),
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < fList.Count; i++)
            {
                var sec = secre.FirstOrDefault(u => u.ID == int.Parse(fList[i].Text)).Id_Institution;
                var inst = _context.INSTITUTIONS.FirstOrDefault(p => p.ID == sec).Name;
                var date = eva.FirstOrDefault(u => u.ID == int.Parse(fList[i].Value)).Date_Received.ToString("dd/MM/yyyy");
                print.Add(inst + "-Pedido; Fecha de entrega: " + date);
            }

            ViewBag.Print = print;
            ViewBag.FList = fList;
            ViewBag.c = (int)DiccionaryB.X;
            ViewBag.u = (int)DiccionaryB.A;

            return View();
        }

        //Method that is used to load the information of the reports according to the id of the evaluation
        [Authorize(Roles = "Gerente")]
        public IActionResult InstitutionReport(string id)
        {

            var eva = _context.EVALUATIONS.FirstOrDefault(u => u.ID == int.Parse(id)).Id_Shipment;

            var shitp = _context.SHIPMENTS.FirstOrDefault(u => u.ID == eva).Id_Request;

            var menus = new List<string>();

            var amounts = new List<string>();

            var rMenu = _context.REQUEST_MENUS.ToList();

            var filtered = rMenu.Where(p => p.Id_Request ==  shitp).ToList();

            var fList = filtered.Select(p => new SelectListItem
            {
                Value = p.Id_Menu.ToString(),
                Text = p.Amount.ToString()
            }).ToList();

            for(int i = (int)DiccionaryB.X; i < fList.Count; i++) {

                var menu = _context.MENUS.FirstOrDefault(u => u.ID == int.Parse(fList[i].Value)).Name;
                menus.Add(menu);

                amounts.Add(fList[i].Text);

            }

            ViewBag.Amounts = amounts;
            ViewBag.Menu = menus;
            ViewBag.rec = _context.EVALUATIONS.FirstOrDefault(u => u.ID == int.Parse(id)).Received;
            ViewBag.state = _context.EVALUATIONS.FirstOrDefault(u => u.ID == int.Parse(id)).Status;
            ViewBag.quan = _context.EVALUATIONS.FirstOrDefault(u => u.ID == int.Parse(id)).Correct_Quantity;
            ViewBag.date = _context.EVALUATIONS.FirstOrDefault((u => u.ID == int.Parse(id))).Date_Received.ToString("dd/MM/yyyy");
            ViewBag.zero = (int)DiccionaryB.X;

            return View();
        }
        //Method that provides a way to cache the HTTP response generated by the "Error" action method for a specified period of time
        [Authorize(Roles = "Gerente")]
        [ResponseCache(Duration = (int)DiccionaryB.X, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
