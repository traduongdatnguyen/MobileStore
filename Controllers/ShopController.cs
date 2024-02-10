using Microsoft.AspNetCore.Mvc;

namespace MobileStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
