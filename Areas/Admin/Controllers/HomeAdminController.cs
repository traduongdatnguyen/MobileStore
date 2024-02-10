using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MobileStore.Areas.Admin.Models.Authentication;
using MobileStore.Models;

namespace MobileStore.Areas.Admin.Controllers
{
    [Authentication]
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
