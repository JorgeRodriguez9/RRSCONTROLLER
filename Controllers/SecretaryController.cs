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
using RRSCONTROLLER.Models;
using System.Globalization;
using Azure.Core;

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

        //Method that is used to see the delivered shipments and show them to the secretary according to your institution
        [Authorize(Roles = "Secretaria Institucion")]
        public IActionResult Received()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var secretary = _context.SECRETARY_INTSs.FirstOrDefault(p => p.Id_User == user).Id_Institution;
            int count = (int)DiccionaryB.X;
            int ped = (int)DiccionaryB.X;

            var shitp = _context.SHIPMENTS.ToList();
            var request = _context.REQUESTS.ToList();
            var nut = _context.NUTRITIONITS_INTSs.ToList();
            var sdList = new List<string>();
            var sdList1 = new List<string>();
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
                    var eva = _context.EVALUATIONS.FirstOrDefault(u => u.Id_Shipment == int.Parse(shitpList1[i].Value));
                    if (eva != null)
                    {
                        sdList1.Add("Pedido #" + (ped + (int)DiccionaryB.A) + " fecha de entrega: " + shitpList[i].Text);;
                        count = (int)DiccionaryB.X;
                        ped++;
                    }
                    else
                    {
                        sdList.Add("Pedido #" + (ped + (int)DiccionaryB.A) + " fecha de entrega: " + shitpList[i].Text);
                        stList.Add(shitpList1[i].Value);
                        count = (int)DiccionaryB.X;
                        ped++;
                    }

                }
            }

            ViewBag.info = sdList;
            ViewBag.info2 = sdList1;
            ViewBag.ID = stList;
            ViewBag.c = (int)DiccionaryB.X;
            ViewBag.u = (int)DiccionaryB.A;

            return View();
        }


        // Method used to see the status of shipments
        [Authorize(Roles = "Secretaria Institucion")]
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
            int ped = (int)DiccionaryB.X;

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
                        sdList.Add("Pedido #" + (ped + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(req);
                        ped++;
                    }
                    if (req == "NO APROBADO")
                    {
                        sdList.Add("Pedido #" + (ped + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(req);
                        ped++;
                    }
                    if (req == "APROBADO")
                    {
                        var men = shitp.FirstOrDefault(u => u.Id_Request == int.Parse(requestList[i].Value));
                        sdList.Add("Pedido #" + (ped + (int)DiccionaryB.A) + " fecha: " + requestList[i].Text);
                        stList.Add(men.Status);
                        ped++;
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

        //Method used to return the evaluation view keeping the id of the shipment
        [Authorize(Roles = "Secretaria Institucion")]
        public IActionResult Evaluation(string id)
        {
            ViewBag.id = id;

            return View();
        }

        //Method that brings the evaluative data of the shipment, and saves it in the database
        [Authorize(Roles = "Secretaria Institucion")]
        [HttpPost]
        public async Task<IActionResult> Evaluation(string shipment, string quantity, string products, string date, string id)
        {
            try
            {
                TempData.Remove("SuccessMessageE");
                TempData.Remove("ErrorE");
                string userName = HttpContext.Session.GetString("UserName");
                var userA = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var secretary = _context.SECRETARY_INTSs.FirstOrDefault(p => p.Id_User == userA).ID;

                if (shipment != null && id != null && quantity != null && products != null && date != null)
                {

                    var evaluation = new EVALUATION
                    {
                        Received = shipment,
                        Correct_Quantity = quantity,
                        Status = products,
                        Date_Received = DateTime.ParseExact(date, "d-M-yyyy", CultureInfo.InvariantCulture),
                        Id_Shipment = int.Parse(id),
                        Id_Secretary_Inst = secretary
                    };

                    _context.EVALUATIONS.Add(evaluation);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessageE"] = "La evaluacion se ha hecho correctamente";
                    return RedirectToAction("Received");
                }
                else
                {
                    TempData["ErrorE"] = "Te falta informacion para hacer el envio";
                    return RedirectToAction("Received");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorE"] = "Error al guardar en la Base de Datos";
                return RedirectToAction("Received");
            }
        }

        //Method used to exit the user from the RRSCONTROLLER application
        [Authorize(Roles = "Secretaria Institucion")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        //Method used to return the beginning of the secretary and institution
        [Authorize(Roles = "Secretaria Institucion")]
        public IActionResult HomeSecretary()
        {

            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var secretary = _context.SECRETARY_INTSs.FirstOrDefault(p => p.Id_User == user).Id_Institution;
            var inst = _context.INSTITUTIONS.FirstOrDefault(p => p.ID == secretary).Name;

            string imp = "Institucion: " + inst;

            ViewBag.inst = imp;


            return View();
        }

    }
}
