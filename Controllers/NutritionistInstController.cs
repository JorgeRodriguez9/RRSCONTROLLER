using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RRS_Controller.Controllers
{
    public class NutritionistInstController : Controller
    {
        // GET: NutritionistInstController
        public ActionResult Index()
        {
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
