using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;

namespace RRSCONTROLLER.Controllers
{
    public class AdminPaeController : Controller
    {

        private readonly RSSCONTROLLERContext _context;

        public AdminPaeController(RSSCONTROLLERContext context)
        {
            _context = context;
        }
        // GET: AdminPaeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminPaeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminPaeController/Create
        public ActionResult ProductRegister()
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

        // GET: AdminPaeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminPaeController/Edit/5
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

        // GET: AdminPaeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPaeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}

