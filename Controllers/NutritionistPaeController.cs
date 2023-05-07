using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RRSCONTROLLER.Controllers
{

    [Authorize]

    public class NutritionistPaeController : Controller
    {

        private readonly ILogger<NutritionistPaeController> _logger;

        private readonly RSSCONTROLLERContext _context;

        public NutritionistPaeController(RSSCONTROLLERContext context, ILogger<NutritionistPaeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult HomeNutritionistPae()
        {
            return View();
        }

        public IActionResult createMenu()
        {
            return View();
        }

        public IActionResult CreateFood()
        {
            var unitsList = new List<string>();
            var producs = _context.PRODUCTS.ToList();
            var producList = producs.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Complete = producList ?? new List<SelectListItem>();
            for (int i = 0; i < producList.Count; i++)
            {
                var products = _context.PRODUCTS.ToList();
                var filteredRoles = products.FirstOrDefault
                    (r => r.Name == producList[i].Text).Id_Unit;
                var unit = _context.UNITS.FirstOrDefault(u => u.ID == filteredRoles).Name_Unit;
                unitsList.Add(unit);
            }
            ViewBag.units = unitsList;
            return View();
        }

        [HttpPost]
        public IActionResult CreateFood(string name, List<string> selectedFoods, List<string> amount4)
        {

            ViewBag.name = name;
            ViewBag.Food = selectedFoods;
            ViewBag.Amount = amount4;
            List<string> amount = new List<string>();
            for (int i = 0; i < selectedFoods.Count; i++)
            {
                var products = _context.PRODUCTS.ToList();
                var filteredRoles = products.FirstOrDefault
                    (r => r.Name == selectedFoods[i]).Id_Unit;
                var amou = _context.UNITS.FirstOrDefault(u => u.ID == filteredRoles).Name_Unit;
                amount.Add(amou);
            }
            ViewBag.unit = amount;

            return View("RegisterFood");
        }
        public IActionResult registerMenu()
        {
            return View();
        }


        // GET: NutritionistPaeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NutritionistPaeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NutritionistPaeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NutritionistPaeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: NutritionistPaeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NutritionistPaeController/Edit/5
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

        // GET: NutritionistPaeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NutritionistPaeController/Delete/5
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
