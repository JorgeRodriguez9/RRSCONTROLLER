﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RRSCONTROLLER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using static RRSCONTROLLER.DAL.RSSCONTROLLERContext;

namespace RRSCONTROLLER.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Method used to exit the user from the RRSCONTROLLER application
        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        //Method used to return the privacy view
        public IActionResult Privacy()
        {
            return View();
        }

        //Method that provides a way to cache the HTTP response generated by the "Error" action method for a specified period of time
        [ResponseCache(Duration = (int)DiccionaryB.X, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}