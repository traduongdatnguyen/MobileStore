using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;
using MobileStore.Areas.Admin.Models.Authentication;

namespace MobileStore.Areas.Admin.Controllers
{
    [Authentication]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {

        //public ActionResult Upload()
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string fileName = Path.GetFileName(file.FileName);
        //        string uploadPath = Server.MapPath("~/UploadedFiles"); // Đường dẫn đến thư mục lưu trữ tệp

        //        // Tạo thư mục nếu chưa tồn tại
        //        if (!Directory.Exists(uploadPath))
        //        {
        //            Directory.CreateDirectory(uploadPath);
        //        }

        //        string filePath = Path.Combine(uploadPath, fileName);
        //        file.SaveAs(filePath);

        //        // Xử lý thông tin về file sau khi upload thành công
        //        // Ví dụ: lưu đường dẫn vào cơ sở dữ liệu
        //        // ...

        //        ViewBag.Message = "Upload thành công!";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Không có file được chọn!";
        //    }

        //    return View();
        //}
        MobileStoreDbContext mobileStoreDb = new MobileStoreDbContext();

        [Route("")]
        [Route("index")]
        public IActionResult index()
        {
            var lstCategory = mobileStoreDb.Categories.ToList();
            return View(lstCategory);
        }


        [Route("add")]
        public IActionResult AddCategory()
        {
            var categories = mobileStoreDb.Categories.ToList();
            var lastCategory = categories.LastOrDefault();
            return View(lastCategory);
        }

        [Route("add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(category.CategoryImage))
            {
                mobileStoreDb.Categories.Add(category);
                mobileStoreDb.SaveChanges();
                TempData["Message"] = "Thêm danh mục thành công!";
                return RedirectToAction("danhmuc", "admin");
            }
            else
            {
                TempData["Message"] = "Thêm danh mục không thành công!";
                return View(category);
            }
            return View(category);
        }


        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            // Tìm đối tượng Category cần xoá trong cơ sở dữ liệu dựa trên Id được truyền vào
            //SELECT TOP 1 * FROM Categories WHERE category_id = {id}
            var categoryToDelete = mobileStoreDb.Categories.Find(id);


            // Kiểm tra xem đối tượng cần xoá có tồn tại hay không
            if (categoryToDelete != null)
            {
                // Nếu tồn tại, xoá đối tượng này khỏi cơ sở dữ liệu
                mobileStoreDb.Categories.Remove(categoryToDelete);
                mobileStoreDb.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                TempData["Message"] = "Xoá danh mục thành công!";
            }
            else
            {
                TempData["Message"] = "Không thành công!";
            }
            // Sau khi xoá thành công hoặc không thành công, chuyển hướng về action "DanhMuc" trong controller "Admin"

            return RedirectToAction("danhmuc", "admin");
        }

        [Route("edit")]
        public IActionResult EditCategory(int id)
        {
            return View();
        }
    }
}
