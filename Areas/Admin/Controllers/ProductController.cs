
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileStore.Areas.Admin.Models.Authentication;
using MobileStore.Models;
using System.IO;

namespace MobileStore.Areas.Admin.Controllers
{
    [Authentication]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();
       
        [Route("")]
        [Route("index")]
        public IActionResult index()
        {
            var lstproducts = mobileStoreDb.Products.ToList();
            return View(lstproducts);
        }


        [Route("add")]
        [HttpGet]
        public IActionResult themproduct()
        {
            ViewBag.BrandId = new SelectList(mobileStoreDb.Brands.ToList(), "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(mobileStoreDb.Categories.ToList(), "CategoryId", "CategoryName");

            return View();
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> themproduct(Product product)
        {
        
            ViewBag.BrandId = new SelectList(mobileStoreDb.Brands.ToList(), "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(mobileStoreDb.Categories.ToList(), "CategoryId", "CategoryName");

            var file = HttpContext.Request.Form.Files.FirstOrDefault();

           
            if (file != null && file.Length > 0)
            {
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", file.FileName);

                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            if (ModelState.IsValid)
            {
             

                mobileStoreDb.Add(product);
                    mobileStoreDb.SaveChanges();

                    TempData["Message"] = "Thêm sản phẩm thành công!";
                    return RedirectToAction("product", "admin");
                
            }

            return View(product);
        }


        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            // Tìm đối tượng Category cần xoá trong cơ sở dữ liệu dựa trên Id được truyền vào
            //SELECT TOP 1 * FROM Categories WHERE category_id = {id}
            var product = mobileStoreDb.Products.Find(id);


            // Kiểm tra xem đối tượng cần xoá có tồn tại hay không
            if (product != null)
            {
                // Nếu tồn tại, xoá đối tượng này khỏi cơ sở dữ liệu
                mobileStoreDb.Products.Remove(product);
                mobileStoreDb.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                TempData["Message"] = "Xoá sản phẩm thành công!";
            }
            else
            {
                TempData["Message"] = "Không thành công!";
            }
            // Sau khi xoá thành công hoặc không thành công, chuyển hướng về action "DanhMuc" trong controller "Admin"

            return RedirectToAction("product", "admin");
        }


        [Route("edit")]
        [HttpGet]
        public IActionResult ChangeProduct(int ProductId)
        {

            var product = mobileStoreDb.Products.Find(ProductId);

            ViewBag.BrandId = new SelectList(mobileStoreDb.Brands.ToList(), "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(mobileStoreDb.Categories.ToList(), "CategoryId", "CategoryName");
            return View(product);
        }



        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeProduct")]
        public IActionResult ChangeProduct(Product updatedProduct)
        {
            var avataImg = HttpContext.Request.Form.Files.FirstOrDefault();

            if (ModelState.IsValid)
            {
                try
                {
                    if (avataImg != null && avataImg.Length > 0)
                    {
                        var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", avataImg.FileName);
                        using (var stream = new FileStream(imgPath, FileMode.Create))
                        {
                            avataImg.CopyTo(stream);
                        } 
                    
                    }
                    updatedProduct.AvatarImageUrl = avataImg.FileName;
                    mobileStoreDb.Update(updatedProduct);
                    mobileStoreDb.SaveChanges();
                    TempData["Message"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction("product", "admin");
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Xử lý lỗi cập nhật đồng thời nếu có
                    TempData["Message"] = "Đã xảy ra lỗi khi cập nhật sản phẩm.";
                    // Có thể thêm xử lý lỗi khác tùy vào yêu cầu của bạn
                    return View(updatedProduct);
                }

            }
            ViewBag.BrandId = new SelectList(mobileStoreDb.Brands.ToList(), "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(mobileStoreDb.Categories.ToList(), "CategoryId", "CategoryName");
            return View(updatedProduct);
        }

        [HttpPost]
        [ActionName("AddImage")]
        //sử dụng async để thực hiện bất đồng bộ
        public async Task<IActionResult> AddImage()
        {

            var productId = HttpContext.Request.Form["ProductId"];
            var files = HttpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Name == "image_url[]")
                {
                    // Xử lý tệp tin có tên là "image_url[]"
                    if (file.Length > 0)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", file.FileName);

                        var stream = new FileStream(imagePath, FileMode.Create);
                        await file.CopyToAsync(stream);
                        

                        var productImage = new ProductImage
                        {
                            ProductId = int.Parse(productId),
                            ImageUrl = file.FileName
                        };

                        mobileStoreDb.ProductImages.Add(productImage);
                    }
                }
            }
            // Lưu tất cả thay đổi vào cơ sở dữ liệu một lần duy nhất
            await mobileStoreDb.SaveChangesAsync();

            TempData["Message"] = "Cập nhật hình ảnh thành công!";
            return RedirectToAction("product", "admin");
        }


        

    }
}
