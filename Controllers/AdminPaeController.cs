using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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

        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: AdminPaeController/Create
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
        public async Task<IActionResult> ProductRegister(string nombre, string supplierId, string unitId)
        {


            var supplier = _context.SUPPLIERS.FirstOrDefault(p => p.Name == supplierId);
            var unit = _context.UNITS.FirstOrDefault(p => p.Name_Unit == unitId);

            if (supplier != null && unit != null)
            {

                var product = new PRODUCT
                {
                    Name = nombre,
                    Id_Supplier = supplier.ID,
                    Id_Unit = unit.ID,
                    Id_Admin_Pae = 1
                };

                _context.PRODUCTS.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "The product was registered successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while registering the product.";
                ViewBag.supplierId = supplierId;
            }

            return RedirectToAction("ProductRegister");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

