using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;

namespace RRSCONTROLLER.Controllers
{

    [Authorize]
    public class AdminPaeController : Controller
    {
        private readonly ILogger<AdminPaeController> _logger;

        private readonly RSSCONTROLLERContext _context;

        public AdminPaeController(RSSCONTROLLERContext context, ILogger<AdminPaeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: AdminPaeController

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomeAdministrator()
        {
            return View();
        }

        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: AdminPaeController/ProductRegister
        public IActionResult ProductRegister()
        {
            var suppliers = _context.SUPPLIERS.ToList();
            var units = _context.UNITS.ToList();

            // Mapear los proveedores a una lista de SelectListItem
            var suppliersList = suppliers.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.supplier = suppliersList ?? new List<SelectListItem>();

            var unitsList = units.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name_Unit
            }).ToList();

            ViewBag.unit = unitsList ?? new List<SelectListItem>();

            return View();
        }

        // POST: AdminPaeController/Create
        [HttpPost]
        public async Task<IActionResult> ProductRegister(string name, string supplierId, string unitId)
        {

            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var supplier = _context.SUPPLIERS.FirstOrDefault(p => p.Name == supplierId);
            var unit = _context.UNITS.FirstOrDefault(p => p.Name_Unit == unitId);
            var admin = _context.ADMIN_PAEs.FirstOrDefault(p => p.Id_User == user).ID;

            if (supplier != null && unit != null)
            {

                var product = new PRODUCT
                {
                    Name = name,
                    Id_Supplier = supplier.ID,
                    Id_Unit = unit.ID,
                    Id_Admin_Pae = admin
                };

                _context.PRODUCTS.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "The product was registered successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while registering the product.";
            }

            return RedirectToAction("ProductRegister");
        }


        // GET: AdminPaeController/ProductRegister
        public IActionResult UserRegister()
        {
            var role = _context.ROLES.ToList();
            var inst = _context.INSTITUTIONS.ToList();

            var filteredRoles = role.Where(r => r.Name_Role== "Nutritionits Institution" || r.Name_Role == "Secretary Institution");

            // Mapear los proveedores a una lista de SelectListItem
            var roleList = filteredRoles.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name_Role
            }).ToList();

            ViewBag.role = roleList ?? new List<SelectListItem>();

            var instList = inst.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.inst = instList ?? new List<SelectListItem>();
            ViewBag.cero = (int)DiccionaryB.X;

            return View();
        }

        // POST: AdminPaeController/Create
        [HttpPost]
        public async Task<IActionResult> UserRegister(string name, string lastname, string adress, string document,
                                                      string user, string password, string email, string roleId, string instId)
        {
            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var userA = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var admin = _context.ADMIN_PAEs.FirstOrDefault(p => p.Id_User == userA).ID;
                var role = _context.ROLES.FirstOrDefault(p => p.Name_Role == roleId);
                var institution = _context.INSTITUTIONS.FirstOrDefault(p => p.Name == instId);

                if (user != null && password != null && name != null && lastname != null && adress != null && document != null
                    && roleId != null && instId != null)
                {
                    var newUser = new USER
                    {
                        Name_User = user,
                        Password = password,
                        Id_Role = role.ID
                    };

                    _context.USERS.Add(newUser);
                    await _context.SaveChangesAsync();

                    if (role.ID == (int)DiccionaryB.D)
                    {

                        var nutritionitsInst = new NUTRITIONITS_INTS
                        {
                            Name = name,
                            Document = int.Parse(document),
                            Last_Name = lastname,
                            Adress = adress,
                            Email = email,
                            Id_User = _context.USERS.FirstOrDefault(p => p.Name_User == user).ID,
                            Id_Institution = institution.ID,
                            Id_Admin_Pae = admin
                        };

                        _context.NUTRITIONITS_INTSs.Add(nutritionitsInst);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "The product was registered successfully.";
                    }

                    if (role.ID == (int)DiccionaryB.E)
                    {

                        var secretaryInst = new SECRETARY_INTS
                        {
                            Name = name,
                            Document = int.Parse(document),
                            Last_Name = lastname,
                            Adress = adress,
                            Email = email,
                            Id_User = _context.USERS.FirstOrDefault(p => p.Name_User == user).ID,
                            Id_Institution = institution.ID,
                            Id_Admin_Pae = admin
                        };

                        _context.SECRETARY_INTSs.Add(secretaryInst);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "The product was registered successfully.";

                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while registering the product.";
                }

                return RedirectToAction("UserRegister");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("UserRegister");
            }
          
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

