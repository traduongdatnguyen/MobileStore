namespace MobileStore.Models
{
    public class HomeModel
    {
        public List<Category> lstCategories { get; set; }
        public List<Brand> lstBrands { get; set; }

        public List<Banner> lstBanners { get; set; }

        public List<Product> lstProductsFlashSale { get; set; }

        public List<Product> lstProductGoiY { get; set; }

        public Product detailProduct {  get; set; }

        public List<ProductImage> imgProductImage { get; set; }
        public User user { get; set; }

    }
}
