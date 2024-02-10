using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore.Models;
using NuGet.Packaging;
using System.Diagnostics;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        //Goi doi tuong lop connect database
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();

        //Goi doi tuong lop homeModel
        HomeModel homeModel = new HomeModel();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() {


            var listCategories = mobileStoreDb.Categories.ToList();

            var lstBrand = new List<Brand>();
            for (int i = 0; i < listCategories.Count; i++)
            {
                //view loai san pham
                var brands = ViewBrands(i + 1);
           
                lstBrand.AddRange(brands.ToList());
            }

            homeModel.lstBrands = (lstBrand);

            homeModel.lstCategories = listCategories;

            homeModel.lstBanners = ViewBanner();

            homeModel.lstProductsFlashSale = ViewProductsFlashSale();


            homeModel.lstProductGoiY = ViewProductsGoiY();

            return View(homeModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
     
        private List<Brand> ViewBrands(int id)
        {            
            var databrands = mobileStoreDb.Brands.Where(x => x.CategoryId == id).ToList();

            return databrands;
        }

        private List<Banner> ViewBanner() {

            var dataBanners = mobileStoreDb.Banners.Where(x => x.BannerStatus == "on").OrderBy(y => y.BannerDateAdded).ToList();
            return dataBanners;
        }
        private List<Product> ViewProductsFlashSale()
        {
            var dataViewProductsFlashSale = mobileStoreDb.Products.Where(x => x.ProductStatus == "flashsale").ToList();
            return dataViewProductsFlashSale;
        }
        private List<Product> ViewProductsGoiY()
        {
        
            var dataViewProductGoiY = mobileStoreDb.Products.OrderBy(x => Guid.NewGuid()).ThenBy(x => x.CreatedAt).Take(6).ToList();
            return dataViewProductGoiY;
        }
    }
}