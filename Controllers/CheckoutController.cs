using Microsoft.AspNetCore.Mvc;

namespace MobileStore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
