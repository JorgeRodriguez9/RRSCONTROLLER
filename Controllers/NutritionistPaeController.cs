using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RRSCONTROLLER.Controllers
{
    public class NutritionistPaeController : Controller
    {

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
