using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.Controllers;
using RRSCONTROLLER.DAL;

namespace RRS_Controller.Controllers
{
    [Authorize]
    public class NutritionistInstController : Controller
    {

        private readonly ILogger<NutritionistInstController > _logger;

        private readonly RSSCONTROLLERContext _context;

        public NutritionistInstController(RSSCONTROLLERContext context, ILogger<NutritionistInstController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: NutritionistInstController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menus()
        {
            var menu = _context.MENUS.ToList();
            var producList = menu.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Complete = producList ?? new List<SelectListItem>();

            var cate = _context.CATEGORYS.ToList();
            var itemList = cate.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Category = itemList ?? new List<SelectListItem>();

            return View();
        }


        // GET: NutritionistInstController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NutritionistInstController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NutritionistInstController/Create
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

        // GET: NutritionistInstController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult HomeNutritionistINST()
        {
            return View();
        }

        // POST: NutritionistInstController/Edit/5
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

        // GET: NutritionistInstController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NutritionistInstController/Delete/5
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
