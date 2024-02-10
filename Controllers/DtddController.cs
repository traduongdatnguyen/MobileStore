using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore.Models;


namespace MobileStore.Controllers
{
   
    public class DtddController : Controller
    {
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();

        HomeModel homeModel = new HomeModel();
      
        public IActionResult Index()
        {
            var listBrands = mobileStoreDb.Brands.ToList();

            var ListCategories = mobileStoreDb.Categories.ToList();

            var lstBrand = new List<Brand>();
            for (int i = 0; i < ListCategories.Count; i++)
            {
                //view loai san pham
                var brands = ViewBrands(i + 1);

                lstBrand.AddRange(brands.ToList());
            }

            homeModel.lstBrands = lstBrand;

            homeModel.lstCategories = ListCategories;

            homeModel.lstBanners = ViewBanner();


            return View(homeModel);
        }


        [Route("dtdd/detail")]
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult DetailProduct(int id)
        {
            var listBrands = mobileStoreDb.Brands.ToList();

            var ListCategories = mobileStoreDb.Categories.ToList();

            var lstBrand = new List<Brand>();
            for (int i = 0; i < ListCategories.Count; i++)
            {
                //view loai san pham
                var brands = ViewBrands(i + 1);

                lstBrand.AddRange(brands.ToList());
            }

            homeModel.lstBrands = lstBrand;

            homeModel.lstCategories = ListCategories;

            homeModel.lstBanners = ViewBanner();

            var detailProduct = mobileStoreDb.Products.Find(id);

            var imgDetails = mobileStoreDb.ProductImages.Where(p => p.ProductId == id).ToList();

            homeModel.detailProduct = detailProduct;

            homeModel.imgProductImage = imgDetails;

            return View(homeModel);
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
