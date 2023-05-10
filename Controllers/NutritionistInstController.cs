using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.Controllers;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
using System.Globalization;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;

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
        [Authorize(Roles = "Nutritionist Institution")]
        public ActionResult Menus()
        {
            var cate = _context.CATEGORYS.ToList();
            var itemList = cate.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Category = itemList ?? new List<SelectListItem>();

            return View();
        }

        [Authorize(Roles = "Nutritionist Institution")]
        [HttpPost]
        public ActionResult Menus(string categoryId, List<string> selectedMenus)
        {
            TempData.Remove("SuccessMessageR");
            TempData.Remove("ErrorR");
            ViewBag.selectedMenus = selectedMenus;
            ViewBag.SelectCategory = categoryId;
            ViewBag.zero = (int)DiccionaryB.X;

            return View("RegisterRequest");
        }
        [Authorize(Roles = "Nutritionist Institution")]
        public IActionResult RegisterRequest()
        {
            return View();
        }
        
        [Authorize(Roles = "Nutritionist Institution")]
        [HttpPost]
        public async Task<IActionResult> RegisterRequest(List<string> quantity, List<string> menus, string fecha)
        {
            TempData.Remove("SuccessMessageR");
            TempData.Remove("ErrorR");
            int cont = (int)DiccionaryB.X;
            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var nutritionistInst = _context.NUTRITIONITS_INTSs.FirstOrDefault(p => p.Id_User == user).ID;


                if (quantity != null && menus != null && fecha != null)
                {

                    for (int i = (int)DiccionaryB.X; i < menus.Count; i++)
                    {

                        if (menus[i] != null && quantity[i] != null && int.Parse(quantity[i]) > (int)DiccionaryB.X)
                        {
                        }
                        else
                        {
                            cont = (int)DiccionaryB.A;
                        }

                    }

                    if (menus.Count == (int)DiccionaryB.X || quantity.Count == (int)DiccionaryB.X)
                    {
                        cont = (int)DiccionaryB.A;
                    }

                    if (cont == (int)DiccionaryB.X)
                    {

                        var request = new REQUEST
                        {
                            Date = DateTime.ParseExact(fecha, "d-M-yyyy", CultureInfo.InvariantCulture),
                            Status = "RECIBIDO",
                            Desired_Delivery_Date = DateTime.Now,
                            Id_Nutritionits_Ints = nutritionistInst
                        };

                        _context.REQUESTS.Add(request);
                        await _context.SaveChangesAsync();

                        var ultimoProducto = _context.REQUESTS.OrderByDescending(p => p.ID).FirstOrDefault();

                        if (ultimoProducto != null)
                        {
                            var id = ultimoProducto.ID;

                            for (int i = (int)DiccionaryB.X; i < menus.Count; i++)
                            {

                                var requestMenu = new REQUEST_MENU
                                {
                                    Id_Request = id,
                                    Id_Menu = _context.MENUS.FirstOrDefault(u => u.Name == menus[i]).ID,
                                    Amount = int.Parse(quantity[i])
                                };

                                _context.REQUEST_MENUS.Add(requestMenu);
                            }

                            await _context.SaveChangesAsync();
                            TempData["SuccessMessageR"] = "La solicitud se ha creado correctamente se ha registrado exitosamente";
                            return RedirectToAction("Menus");
                        }
                        else
                        {
                            TempData["ErrorR"] = "La solicitud no se guardo correctamente en la BD";
                            return RedirectToAction("Menus");
                        }
                        
                    }
                    else
                    {
                        TempData["ErrorR"] = "La solicitud le falta informacion";
                        return RedirectToAction("Menus");
                    }

                }
                else
                {

                    TempData["ErrorR"] = "La solicitud le falta informacion";
                    return RedirectToAction("Menus");

                }

            }
            catch (System.Exception e)
            {
                TempData["ErrorR"] = "Error al guardar en la BD";
                return RedirectToAction("Menus");
            }
        }
        


        [Authorize(Roles = "Nutritionist Institution")]
        public IActionResult HomeNutritionistINST()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MyAction(string text, string id)
        {
            var menu = _context.MENUS.FirstOrDefault(u => u.Name == text).ID;

            var foodList = new List<string>();
            var pList = new List<List<String>>();

            var mfood = _context.MENU_FOODS.ToList();
            var pfood = _context.FOOD_PRODUCTS.ToList();

            var food = _context.FOODS.ToList();

            var filteredRoles = mfood.Where(r => r.Id_Menu == menu);

            // Mapear los proveedores a una lista de SelectListItem
            var roleList = filteredRoles.Select(p => new SelectListItem
            {
                Value = p.Id_Food.ToString()
            }).ToList();

            for (int i = (int)DiccionaryB.X; i < roleList.Count; i++)
            {
                var filteredFood = food.FirstOrDefault(r => r.ID == int.Parse(roleList[i].Value)).Name;
                foodList.Add(filteredFood);
            }

            for (int i = (int)DiccionaryB.X; i < foodList.Count; i++)
            {

                var product = new List<string>();

                var foods = food.FirstOrDefault(r => r.Name == foodList[i]).ID;

                var filteredMenu = pfood.Where(r => r.Id_Food == foods);

                var productList = filteredMenu.Select(p => new SelectListItem
                {
                    Value = p.Id_Product.ToString(),
                    Text = p.Amount.ToString()
                }).ToList();

                for (int j = (int)DiccionaryB.X; j < productList.Count; j++)
                {
                    var filteredP = productList[j].Value + " " + _context.PRODUCTS.FirstOrDefault(r => r.ID == int.Parse(productList[j].Value)).Name;
                    product.Add(filteredP);
                }

                pList.Add(product);

            }

            ViewBag.Foods = foodList;
            ViewBag.Products = pList;

            return PartialView("_SelectedItemsBox");

        }

        [HttpPost]
        public IActionResult GetMenu(string menuCategory)
        {

            var category = _context.CATEGORYS.FirstOrDefault(u => u.Name == menuCategory).ID;
            var menu = _context.MENUS.ToList();
            var filteredMenu = menu.Where(r => r.Id_Category == category);
            var producList = filteredMenu.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Complete = producList ?? new List<SelectListItem>();
            return PartialView("_MenuInformation");
        }

    }
}
