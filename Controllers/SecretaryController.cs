using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.Controllers;
using RRSCONTROLLER.DAL;

namespace RRS_Controller.Controllers
{

    [Authorize]
    public class SecretaryController : Controller
    {

        private readonly ILogger<SecretaryController> _logger;

        private readonly RSSCONTROLLERContext _context;

        public SecretaryController(RSSCONTROLLERContext context, ILogger<SecretaryController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: SecretaryController

        public IActionResult OrderStatus()
        {
            return View();
        }

        public IActionResult Received()
        {
            return View();
        }

        public IActionResult Evaluation()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: SecretaryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SecretaryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecretaryController/Create
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

        // GET: SecretaryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult HomeSecretary()
        {
            return View();
        }

        // POST: SecretaryController/Edit/5
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

        // GET: SecretaryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SecretaryController/Delete/5
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
