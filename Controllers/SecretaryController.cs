using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.Controllers;
using RRSCONTROLLER.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;
using System;

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

        [Authorize(Roles = "Secretary Institution")]
        public IActionResult Received()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var secretary = _context.SECRETARY_INTSs.FirstOrDefault(p => p.Id_User == user).Id_Institution;
            int count = (int)DiccionaryB.X;

            var shitp = _context.SHIPMENTS.ToList();
            var request = _context.REQUESTS.ToList();
            var nut = _context.NUTRITIONITS_INTSs.ToList();
            var sdList = new List<string>();
            var stList = new List<string>();

            var filteredShip = shitp.Where(u => u.Status == "ENTREGADO");

            var shitpList = filteredShip.Select(p => new SelectListItem
            {
                Value = p.Id_Request.ToString(),
                Text = p.Date.ToString("dd/MM/yyyy"),
            }).ToList();

            var shitpList1 = filteredShip.Select(p => new SelectListItem
            {
                Value = p.ID.ToString()
            }).ToList();

            var filtered = nut.Where(u => u.Id_Institution == secretary);

            var fList = filtered.Select(p => new SelectListItem
            {
                Value = p.ID.ToString()
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < shitpList.Count; i++)
            {
                var nutri = request.FirstOrDefault(u => u.ID == int.Parse(shitpList[i].Value)).Id_Nutritionits_Ints;
                for (int j = (int)DiccionaryB.X; j < fList.Count; j++)
                {
                    if (nutri == int.Parse(fList[j].Value))
                    {
                        count = (int)DiccionaryB.A;
                    }
                }

                if (count == (int)DiccionaryB.A)
                {
                    sdList.Add("Pedido #" + (i + (int)DiccionaryB.A) + " fecha de entrega: " + shitpList[i].Text);
                    stList.Add(shitpList1[i].Value);
                    count = (int)DiccionaryB.X;
                }
            }
            
            ViewBag.info = sdList;
            ViewBag.ID = stList;
            ViewBag.c = (int)DiccionaryB.X;
            ViewBag.u = (int)DiccionaryB.A;

            return View();
        }


        // GET: SecretaryController
        [Authorize(Roles = "Secretary Institution")]
        public IActionResult OrderStatus()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var secretary = _context.SECRETARY_INTSs.FirstOrDefault(p => p.Id_User == user).Id_Institution;
            int count = (int)DiccionaryB.X;

            var request = _context.REQUESTS.ToList();
            var shitp = _context.SHIPMENTS.ToList();
            var nut = _context.NUTRITIONITS_INTSs.ToList();
            var sdList = new List<string>();
            var stList = new List<string>();

            var filtered = nut.Where(u => u.Id_Institution == secretary);
            var fList = filtered.Select(p => new SelectListItem
            {
                Value = p.ID.ToString()
            }).ToList();


            var requestList = request.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Date.ToString("dd/MM/yyyy"),
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < requestList.Count; i++)
            {
                var nutri = request.FirstOrDefault(u => u.ID == int.Parse(requestList[i].Value)).Id_Nutritionits_Ints;

                for (int j = (int)DiccionaryB.X; j < fList.Count; j++)
                {
                    if(nutri == int.Parse(fList[j].Value))
                    {
                        count = (int)DiccionaryB.A;
                    }
                }
                if(count == (int)DiccionaryB.A)
                {
                    var req = request.FirstOrDefault(u => u.ID == int.Parse(requestList[i].Value)).Status;
                    if (req == "RECIBIDO")
                    {
                        sdList.Add("Pedido #" + (i + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(req);
                    }
                    if (req == "NO APROBADO")
                    {
                        sdList.Add("Pedido #" + (i + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(req);
                    }
                    if (req == "APROBADO")
                    {
                        var men = shitp.FirstOrDefault(u => u.Id_Request == int.Parse(requestList[i].Value));
                        sdList.Add("Pedido #" + (i + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(men.Status);
                    }
                    count = (int)DiccionaryB.X;
                }
                
            }

            ViewBag.info = sdList;
            ViewBag.status = stList;
            ViewBag.c = (int)DiccionaryB.X;
            ViewBag.u = (int)DiccionaryB.A;

            return View();
        }
        [Authorize(Roles = "Secretary Institution")]
        public IActionResult Evaluation(string id)
        {
            ViewBag.id = id;

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Secretary Institution")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles = "Secretary Institution")]
        public IActionResult HomeSecretary()
        {
            return View();
        }

    }
}
