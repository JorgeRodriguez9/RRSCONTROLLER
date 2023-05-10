using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RRSCONTROLLER.Controllers;
using RRSCONTROLLER.DAL;
using RRSCONTROLLER.Models;
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

        [Authorize(Roles = "Nutritionist Institution")]
        [HttpPost]
        public ActionResult Menus(string categoryId, List<string> selectedMenus)
        {
            TempData.Remove("SuccessMessage");
            TempData.Remove("Error");
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
        /*
        [Authorize(Roles = "Nutritionist Institution")]
        [HttpPost]
        public async Task<IActionResult> RegisterRequest(List<string> foods, string name, string category)
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
        */


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

    }
}
