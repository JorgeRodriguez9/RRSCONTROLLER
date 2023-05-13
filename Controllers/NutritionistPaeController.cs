using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.Models;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Method used to return the start nutritionist pae view
        [Authorize(Roles = "Nutricionista Pae")]
        public IActionResult HomeNutritionistPae()
        {
            return View();
        }

        //Method that serves to return the categories and foods brought from the database and show them in the create menu
        [Authorize(Roles = "Nutricionista Pae")]
        public IActionResult CreateMenu()
        {
            
            var food = _context.FOODS.ToList();
            var foodList = food.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.food = foodList ?? new List<SelectListItem>();

            var category = _context.CATEGORYS.ToList();
            var categoryList = category.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.category = categoryList ?? new List<SelectListItem>();
            ViewBag.cero = (int)DiccionaryB.X;

            return View();

        }

        //Method used to extract the menu data from the createmenu view and send them to the registermenu view
        [Authorize(Roles = "Nutricionista Pae")]
        [HttpPost]
        public IActionResult CreateMenu(string category, string name, List<string> selectedFoods)
        {
            TempData.Remove("SuccessMessage");
            TempData.Remove("Error");
            ViewBag.name = name;
            ViewBag.selectFood = selectedFoods;
            ViewBag.SelectCategory = category;
            ViewBag.zero = (int)DiccionaryB.X;

            return View("RegisterMenu");
        }

        //method used to return the registermenu view
        [Authorize(Roles = "Nutricionista Pae")]
        public IActionResult RegisterMenu()
        {
            return View();
        }

        //Method used to record the menu prepared by the nutritionist in the database
        [Authorize(Roles = "Nutricionista Pae")]
        [HttpPost]
        public async Task<IActionResult> RegisterMenu(List<string> foods, string name, string category)
        {
            TempData.Remove("SuccessMessageM");
            TempData.Remove("ErrorM");
            int cont = (int)DiccionaryB.X;
            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var nutritionistPae = _context.NUTRITIONITS_PAEs.FirstOrDefault(p => p.Id_User == user).ID;


                if (name != null && foods != null && category != null)
                {

                    for (int i = (int)DiccionaryB.X; i < foods.Count; i++)
                    {

                        if (foods[i] != null)
                        {
                        }
                        else
                        {
                            cont = (int)DiccionaryB.A;
                        }

                    }

                    if (foods.Count == (int)DiccionaryB.X)
                    {
                        cont = (int)DiccionaryB.A;
                    }

                    if (cont == (int)DiccionaryB.X)
                    {
                        var idcategory = _context.CATEGORYS.FirstOrDefault(p => p.Name == category).ID;

                        var menu = new MENU
                        {
                            Name = name,
                            Id_Category = idcategory,
                            Id_Nutritionits_Pae = nutritionistPae
                        };

                        _context.MENUS.Add(menu);
                        await _context.SaveChangesAsync();

                        for (int i = (int)DiccionaryB.X; i < foods.Count; i++)
                        {

                            var menuFood = new MENU_FOOD
                            {

                                Id_Menu = _context.MENUS.FirstOrDefault(u => u.Name == name).ID,
                                Id_Food = _context.FOODS.FirstOrDefault(u => u.Name == foods[i]).ID,
                            };

                            _context.MENU_FOODS.Add(menuFood);
                        }

                        await _context.SaveChangesAsync();
                        TempData["SuccessMessageM"] = "El menu se ha registrado exitosamente";
                        return RedirectToAction("CreateMenu");
                    }
                    else
                    {
                        TempData["ErrorM"] = "El menu le falta informacion";
                        return RedirectToAction("CreateMenu");
                    }

                }
                else
                {

                    TempData["ErrorM"] = "El menu le falta informacion";
                    return RedirectToAction("CreateMenu");

                }

            }
            catch (System.Exception e)
            {
                TempData["ErrorM"] = "Error al guardar en la BD";
                return RedirectToAction("CreateMenu");
            }
        }


        //Method used to display in the createmenu view the foods with their units offered by the RRSCONTROLLER application
        [Authorize(Roles = "Nutricionista Pae")]
        public IActionResult CreateFood()
        {
           
            var unitsList = new List<string>();
            var producs = _context.PRODUCTS.ToList();
            var producList = producs.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Complete = producList ?? new List<SelectListItem>();
            for (int i = (int)DiccionaryB.X; i < producList.Count; i++)
            {
                var products = _context.PRODUCTS.ToList();
                var filteredRoles = products.FirstOrDefault
                    (r => r.Name == producList[i].Text).Id_Unit;
                var unit = _context.UNITS.FirstOrDefault(u => u.ID == filteredRoles).Name_Unit;
                unitsList.Add(unit);
            }
            ViewBag.units = unitsList;
            return View();
        }

        //Method that serves to attract the products chosen by the nutritionist and send them to registerfood
        [Authorize(Roles = "Nutricionista Pae")]
        [HttpPost]
        public IActionResult CreateFood(string name, List<string> selectedFoods, List<string> amount4)
        {
            TempData.Remove("SuccessMessage");
            TempData.Remove("Error");
            ViewBag.name = name;
            ViewBag.Food = selectedFoods;
            ViewBag.Amount = amount4;
            List<string> amount = new List<string>();
            for (int i = (int)DiccionaryB.X; i < selectedFoods.Count; i++)
            {
                var products = _context.PRODUCTS.ToList();
                var filteredRoles = products.FirstOrDefault
                    (r => r.Name == selectedFoods[i]).Id_Unit;
                var amou = _context.UNITS.FirstOrDefault(u => u.ID == filteredRoles).Name_Unit;
                amount.Add(amou);
            }
            ViewBag.unit = amount;
            ViewBag.zero = (int)DiccionaryB.X;

            return View("RegisterFood");
        }

        //Method used to return the registerfood view, sending a zero to avoid hardcoding
        [Authorize(Roles = "Nutricionista Pae")]
        public IActionResult RegisterFood()
        {
            ViewBag.zero = (int)DiccionaryB.X;

            return View();
        }

        //Method used to register the food in the database according to the products chosen by the nutritionist
        [Authorize(Roles = "Nutricionista Pae")]
        [HttpPost]
        public async Task<IActionResult> RegisterFood(List<string> foods, List<string> amounts, List<string> units, string name)
        {

            TempData.Remove("SuccessMessage");
            TempData.Remove("Error");
            int cont = (int)DiccionaryB.X;

            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                var user = _context.USERS.FirstOrDefault(p => p.Name_User == userName).ID;
                var nutritionistPae = _context.NUTRITIONITS_PAEs.FirstOrDefault(p => p.Id_User == user).ID;

                if (name != null && foods != null && amounts != null && units != null)
                {
                    for (int i = (int)DiccionaryB.X; i < foods.Count; i++)
                    {
                        if (amounts[i] != null)
                        {
                        }
                        else
                        {
                            cont = (int)DiccionaryB.A;
                        }
                        if (foods[i] != null)
                        {
                        }
                        else
                        {
                            cont = (int)DiccionaryB.A;
                        }
                    }

                    if (foods.Count == (int)DiccionaryB.X)
                    {
                        cont = (int)DiccionaryB.A;
                    }

                    if (cont == (int)DiccionaryB.X) {

                        var food = new FOOD
                        {
                            Name = name,
                            Id_Nutritionits_Pae = nutritionistPae
                        };

                        _context.FOODS.Add(food);
                        await _context.SaveChangesAsync();

                        for (int i = (int)DiccionaryB.X; i < foods.Count; i++)
                        {

                            var foodProduct = new FOOD_PRODUCT
                            {
                                Amount = int.Parse(amounts[i]), 
                                Id_Food = _context.FOODS.FirstOrDefault(u => u.Name == name).ID,
                                Id_Product = _context.PRODUCTS.FirstOrDefault(u => u.Name == foods[i]).ID,
                            };

                            _context.FOOD_PRODUCTS.Add(foodProduct);
                        }

                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "El alimento se ha registrado exitosamente";
                        return RedirectToAction("CreateFood");

                    }
                    else
                    {
                        TempData["Error"] = "El alimento le falta informacion";
                        return RedirectToAction("CreateFood");
                    }

                }
                else
                {

                    TempData["Error"] = "El alimento le falta informacion";
                    return RedirectToAction("CreateFood");

                }

            }
            catch (System.Exception e)
            {
                TempData["Error"] = "Error al guardar en la BD";
                return RedirectToAction("CreateFood");
            }
        }

        //Method used to exit the user from the RRSCONTROLLER application
        [Authorize(Roles = "Nutricionista Pae")]
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

      
    }
}
