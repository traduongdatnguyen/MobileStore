﻿using Microsoft.AspNetCore.Mvc;

namespace MobileStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
