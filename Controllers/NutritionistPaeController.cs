using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.DAL;

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

        public IActionResult createFood()
        {
            return View();
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
