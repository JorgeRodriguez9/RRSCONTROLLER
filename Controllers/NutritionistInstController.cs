using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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

        //Method used to load the different food categories in the combobox of the menus view
        [Authorize(Roles = "Nutricionista Institucion")]
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

        //Method used to receive the category and the different menus chosen by the nutritionist and reflect them in the RegisterRequest view
        [Authorize(Roles = "Nutricionista Institucion")]
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

        //Method used to display the RegisterRequest view
        [Authorize(Roles = "Nutricionista Institucion")]
        public IActionResult RegisterRequest()
        {
            return View();
        }

        //Method used to record the request made by the nutritionist in the database
        [Authorize(Roles = "Nutricionista Institucion")]
        [HttpPost]
        public async Task<IActionResult> RegisterRequest(List<string> quantity, List<string> menus, string date)
        {
            TempData.Remove("SuccessMessageR");
            TempData.Remove("ErrorR");
            int cont = (int)DiccionaryB.X;
            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var nutritionistInst = _context.NUTRITIONITS_INTSs.FirstOrDefault(p => p.Id_User == user).ID;


                if (quantity != null && menus != null && date != null)
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
                            Date = DateTime.Now,
                            Status = "RECIBIDO",
                            Desired_Delivery_Date = DateTime.ParseExact(date, "d-M-yyyy", CultureInfo.InvariantCulture),
                            Id_Nutritionits_Ints = nutritionistInst
                        };

                        _context.REQUESTS.Add(request);
                        await _context.SaveChangesAsync();

                        var lastProduct = _context.REQUESTS.OrderByDescending(p => p.ID).FirstOrDefault();

                        if (lastProduct != null)
                        {
                            var id = lastProduct.ID;

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

        //Method used to return the home view of the nutritionist

        [Authorize(Roles = "Nutricionista Institucion")]
        public IActionResult HomeNutritionistINST()
        {

            string userName = HttpContext.Session.GetString("UserName");
            var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
            var nut = _context.NUTRITIONITS_INTSs.FirstOrDefault(p => p.Id_User == user).Id_Institution;
            var inst = _context.INSTITUTIONS.FirstOrDefault(p => p.ID == nut).Name;

            string imp = "Institucion: " + inst;

            ViewBag.inst = imp;

            return View();
        }

        //Method used to show the different foods and products to the nutritionist
        [Authorize(Roles = "Nutricionista Institucion")]
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

        //Method used to obtain all the different menus provided by the RRSCONTROLLER application
        [Authorize(Roles = "Nutricionista Institucion")]
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

        //Method used to exit the user from the RRSCONTROLLER application
        [Authorize(Roles = "Nutricionista Institucion")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
