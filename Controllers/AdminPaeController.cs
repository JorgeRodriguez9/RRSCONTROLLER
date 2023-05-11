using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;
using Azure.Core;

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
        [Authorize(Roles = "Admin Pae")]
        public IActionResult HomeAdministrator()
        {
            return View();
        }

        public IActionResult Enviar()
        {
            var menu = _context.MENUS.ToList();
            var producList = menu.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Menu = producList ?? new List<SelectListItem>();

            var amount = _context.REQUEST_MENUS.ToList();
            var iLList = amount.Select(q => new SelectListItem
            {
                Value = q.ID.ToString(),
                Text = q.Amount.ToString()
            }).ToList();
            ViewBag.Amount = iLList ?? new List<SelectListItem>();

            return View();
        }

        [Authorize(Roles = "Admin Pae")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: AdminPaeController/ProductRegister
        [Authorize(Roles = "Admin Pae")]
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
        [Authorize(Roles = "Admin Pae")]
        [HttpPost]
        public async Task<IActionResult> ProductRegister(string name, string supplierId, string unitId)
        {

            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var supplier = _context.SUPPLIERS.FirstOrDefault(p => p.Name == supplierId);
            var unit = _context.UNITS.FirstOrDefault(p => p.Name_Unit == unitId);
            var admin = _context.ADMIN_PAEs.FirstOrDefault(p => p.Id_User == user).ID;

            if (supplier != null && unit != null)
            {

                var product = new PRODUCT
                {
                    Name = name,
                    Id_Supplier = supplier.ID,
                    Id_Unit = unit.ID,
                    Id_Admin_Pae = admin
                };

                _context.PRODUCTS.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto fue registrado correctamente.";

            }
            else
            {
                TempData["ErrorMessage"] = "Ocurrio un error al momento de registrar el producto.";
            }

            return RedirectToAction("ProductRegister");
        }


        // GET: AdminPaeController/ProductRegister
        [Authorize(Roles = "Admin Pae")]
        public IActionResult UserRegister()
        {
            var role = _context.ROLES.ToList();
            var inst = _context.INSTITUTIONS.ToList();

            var filteredRoles = role.Where(r => r.Name_Role== "Nutritionist Institution" || r.Name_Role == "Secretary Institution");

            // Mapear los proveedores a una lista de SelectListItem
            var roleList = filteredRoles.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name_Role
            }).ToList();

            ViewBag.role = roleList ?? new List<SelectListItem>();

            var instList = inst.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.inst = instList ?? new List<SelectListItem>();
            ViewBag.cero = (int)DiccionaryB.X;

            return View();
        }

        // POST: AdminPaeController/Create
        [Authorize(Roles = "Admin Pae")]
        [HttpPost]
        public async Task<IActionResult> UserRegister(string name, string lastname, string adress, string document,
                                                      string user, string password, string email, string roleId, string instId)
        {
            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var userA = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var admin = _context.ADMIN_PAEs.FirstOrDefault(p => p.Id_User == userA).ID;
                var role = _context.ROLES.FirstOrDefault(p => p.Name_Role == roleId);
                var institution = _context.INSTITUTIONS.FirstOrDefault(p => p.Name == instId);

                if (user != null && password != null && name != null && lastname != null && adress != null && document != null
                    && roleId != null && instId != null)
                {
                    var newUser = new USER
                    {
                        Name_User = user,
                        Password = password,
                        Id_Role = role.ID
                    };

                    _context.USERS.Add(newUser);
                    await _context.SaveChangesAsync();

                    if (role.ID == (int)DiccionaryB.D)
                    {

                        var nutritionitsInst = new NUTRITIONITS_INTS
                        {
                            Name = name,
                            Document = int.Parse(document),
                            Last_Name = lastname,
                            Adress = adress,
                            Email = email,
                            Id_User = _context.USERS.FirstOrDefault(p => p.Name_User == user).ID,
                            Id_Institution = institution.ID,
                            Id_Admin_Pae = admin
                        };

                        _context.NUTRITIONITS_INTSs.Add(nutritionitsInst);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "El usuario nutricionista fue registrado correctamente.";
                    }

                    if (role.ID == (int)DiccionaryB.E)
                    {

                        var secretaryInst = new SECRETARY_INTS
                        {
                            Name = name,
                            Document = int.Parse(document),
                            Last_Name = lastname,
                            Adress = adress,
                            Email = email,
                            Id_User = _context.USERS.FirstOrDefault(p => p.Name_User == user).ID,
                            Id_Institution = institution.ID,
                            Id_Admin_Pae = admin
                        };

                        _context.SECRETARY_INTSs.Add(secretaryInst);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "El usuario secretario fue registrado correctamente.";

                    }

                }
                else
                {
                    TempData["ErrorMessage"] = "Ocurrio un error al momento de registrar el usuario";
                }

                return RedirectToAction("UserRegister");
            }
            catch (System.Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("UserRegister");
            }
          
        }


        [Authorize(Roles = "Admin Pae")]
        public IActionResult Requests()
        {
            var request = _context.REQUESTS.ToList();
            var units = _context.UNITS.ToList();
            var nut = _context.NUTRITIONITS_INTSs.ToList();
            var send = _context.SHIPMENTS.ToList();
            var impList = new List<string>();
            var sdList = new List<string>();

            var filterRequest = request.Where(r => r.Status == "RECIBIDO");
            var filterSend = send.Where(r => r.Status == "ENVIADO");

            // Mapear los proveedores a una lista de SelectListItem
            var requestList = filterRequest.Select(p => new SelectListItem
            {
                Value = p.Id_Nutritionits_Ints.ToString(),
                Text = p.Date.ToString("dd/MM/yyyy"),
            }).ToList();

            var sendList = filterSend.Select(p => new SelectListItem
            {
                Value = p.Id_Request.ToString(),
                Text = p.Date.ToString("dd/MM/yyyy")
            }).ToList();

            var sList = filterSend.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < sendList.Count; i++)
            {
                var req = request.FirstOrDefault(u => u.ID == int.Parse(sendList[i].Value)).Id_Nutritionits_Ints;
                var nuti = nut.FirstOrDefault(u => u.ID == req).Id_Institution;
                var instText = _context.INSTITUTIONS.FirstOrDefault(u => u.ID == nuti).Name;
                sdList.Add(instText + "- Pedido; fecha de enviado: " + sendList[i].Text);
            }

            var reqList = filterRequest.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < requestList.Count; i++)
            {
                var inst = nut.FirstOrDefault(u => u.ID == int.Parse(requestList[i].Value)).Id_Institution;
                var instText = _context.INSTITUTIONS.FirstOrDefault(u => u.ID == inst).Name;
                impList.Add(instText + "- Pedido; fecha: " + requestList[i].Text);
            }

            ViewBag.ID = reqList;
            ViewBag.request = impList;
            ViewBag.sd = sdList;
            ViewBag.idS = sList;
            ViewBag.c = (int)DiccionaryB.X;

            return View();
        }

        // GET: Tripulants/Details/5
        [Authorize(Roles = "Admin Pae")]
        public async Task<IActionResult> Send(string id)
        {
            var menusR = _context.REQUEST_MENUS.ToList();
            var date = _context.REQUESTS.FirstOrDefault(u => u.ID == int.Parse(id)).Desired_Delivery_Date.ToString("dd/MM/yyyy");
            var impList = new List<string>();

            var filteredMenus = menusR.Where(u => u.Id_Request == int.Parse(id));
            var requestList = filteredMenus.Select(p => new SelectListItem
            {
                Value = p.Id_Menu.ToString(),
                Text = p.Amount.ToString()
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < requestList.Count; i++)
            {
                var menu = _context.MENUS.FirstOrDefault(u => u.ID == int.Parse(requestList[i].Value)).Name;
                impList.Add(requestList[i].Text + " - " + menu);
            }

            ViewBag.selectFood = impList;
            ViewBag.zero = (int)DiccionaryB.X;
            ViewBag.fecha = date;
            ViewBag.idR = id;

            return View();
        }
        [Authorize(Roles = "Admin Pae")]
        public async Task<IActionResult> Deliver(string id)
        {
            try
            {
                TempData.Remove("SuccessMessageS");
                TempData.Remove("ErrorS");

                if (id != null)
                {
                    var ship = _context.SHIPMENTS.FirstOrDefault(u => u.ID == int.Parse(id));

                    ship.Status = "ENTREGADO";

                    _context.SHIPMENTS.Update(ship);

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessageS"] = "Se notifico que el envio fue enviado";
                    return RedirectToAction("Requests");
                }
                else
                {
                    TempData["ErrorS"] = "ERROR";
                    return RedirectToAction("Requests");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorS"] = "Error en la Base de Datos";
                return RedirectToAction("Requests");
            }

        }

        [Authorize(Roles = "Admin Pae")]
        [HttpPost]
        public async Task<IActionResult> Send(string question, string myText, string id)
        {
            try {
                TempData.Remove("SuccessMessageS");
                TempData.Remove("ErrorS");
                string userName = HttpContext.Session.GetString("UserName");
                var userA = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var admin = _context.ADMIN_PAEs.FirstOrDefault(p => p.Id_User == userA).ID;

                if (question != null && id != null)
                {

                    var request = _context.REQUESTS.FirstOrDefault(u => u.ID == int.Parse(id));

                    if (question == "NO")
                    {

                        request.Status = "NO APROBADO";
                        _context.REQUESTS.Update(request);

                        await _context.SaveChangesAsync();

                        TempData["SuccessMessageS"] = "La solicitud ya fue negada";
                        return RedirectToAction("Requests");
                    }
                    else
                    {
                        if (myText != null)
                        {
                            var shipment = new SHIPMENT
                            {
                                Date = DateTime.Now,
                                Status = "ENVIADO",
                                Transport = myText,
                                Id_Request = int.Parse(id),
                                Id_Admin_Pae = admin

                            };

                            request.Status = "APROBADO";
                            _context.REQUESTS.Update(request);
                            _context.SHIPMENTS.Add(shipment);

                            await _context.SaveChangesAsync();
                            TempData["SuccessMessageS"] = "La solicitud ya fue enviada";
                            return RedirectToAction("Requests");
                        }
                        else
                        {
                            TempData["ErrorS"] = "Te falta informacion del transporte";
                            return RedirectToAction("Requests");
                        }
                    }

                }
                else
                {
                    TempData["ErrorS"] = "Te falta informacion para hacer el envio";
                    return RedirectToAction("Requests");
                }
            }
            catch(Exception e) {
                TempData["ErrorS"] = "Error al guardar en la Base de Datos";
                return RedirectToAction("Requests");
            }
            

        }

        [Authorize(Roles = "Admin Pae")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

