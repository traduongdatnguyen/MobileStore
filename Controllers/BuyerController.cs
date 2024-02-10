using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    public class BuyerController : Controller
    {
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();
        HomeModel homeModel = new HomeModel();
        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Admin") == null ) {
                var listBrands = mobileStoreDb.Brands.ToList();

                var ListCategories = mobileStoreDb.Categories.ToList();

                var lstBrand = new List<Brand>();
                for (int i = 0; i < ListCategories.Count; i++)
                {
                    //view loai san pham
                    var brands = ViewBrands(i + 1);

                    lstBrand.AddRange(brands.ToList());
                }

                homeModel.lstBrands = (lstBrand);

                homeModel.lstCategories = ListCategories;

                homeModel.lstBanners = ViewBanner();

                return View(homeModel);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }

        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var loginAction = mobileStoreDb.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (loginAction != null )
            {
                if (HttpContext.Session.GetString("Username") == null)
                {
                    if (loginAction != null && loginAction.RoleId == 1)
                    {
                        HttpContext.Session.SetString("Admin", user.Username.ToString());
                        return RedirectToAction("Index", "admin");
                    }
                    else if (loginAction != null && loginAction.RoleId == 2)
                    {
                        HttpContext.Session.SetString("UserName", user.Username.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                TempData["Message"] = "Vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu!";
                return RedirectToAction("Index", "Buyer");
            }
            return View();
        }

        private List<Brand> ViewBrands(int id)
        {
            var databrands = mobileStoreDb.Brands.Where(x => x.CategoryId == id).ToList();

            return databrands;
        }

        private List<Banner> ViewBanner()
        {

            var dataBanners = mobileStoreDb.Banners.Where(x => x.BannerStatus == "on").OrderBy(y => y.BannerDateAdded).ToList();
            return dataBanners;
        }
    }
}
