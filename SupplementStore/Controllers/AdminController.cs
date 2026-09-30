using Microsoft.AspNetCore.Mvc;
using SupplementStore.Models;

namespace SupplementStore.Controllers
{
    // لوحة إدارة المتجر: إضافة المنتجات وتعديلها وحذفها وإدارة الصور والفئات
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // قائمة المنتجات في لوحة الإدارة
        public IActionResult Index()
        {
            ViewBag.Categories = ProductRepository.AllCategories;
            return View(ProductRepository.Products);
        }

        // ---------- عرض نموذج منتج جديد ----------
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = ProductRepository.AllCategories;
            return View(new ProductInputModel());
        }

        // ---------- حفظ منتج جديد ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            ValidateImage(model);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = ProductRepository.AllCategories;
                return View(model);
            }

            var product = model.ToProduct();
            product.ImageUrl = await SaveUploadedFileAsync(model.ImageFile, product.ImageUrl);
            ProductRepository.Add(product);

            TempData["Msg"] = $"✔ تمت إضافة المنتج «{product.Name}» بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // ---------- عرض نموذج التعديل ----------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var p = ProductRepository.GetById(id);
            if (p == null) return NotFound();

            ViewBag.Categories = ProductRepository.AllCategories;
            var model = new ProductInputModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                OldPrice = p.OldPrice,
                Rating = p.Rating,
                ReviewCount = p.ReviewCount,
                ImageEmoji = p.ImageEmoji,
                ImageUrl = p.ImageUrl,
                InStock = p.InStock
            };
            return View(model);
        }

        // ---------- حفظ التعديل ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductInputModel model)
        {
            ValidateImage(model);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = ProductRepository.AllCategories;
                return View(model);
            }

            var updated = model.ToProduct();
            updated.ImageUrl = await SaveUploadedFileAsync(model.ImageFile, updated.ImageUrl);

            if (!ProductRepository.Update(updated)) return NotFound();

            TempData["Msg"] = $"✔ تم تحديث المنتج «{updated.Name}»";
            return RedirectToAction(nameof(Index));
        }

        // ---------- الحذف ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var p = ProductRepository.GetById(id);
            if (p == null) return NotFound();

            ProductRepository.Delete(id);
            TempData["Msg"] = $"🗑 تم حذف المنتج «{p.Name}»";
            return RedirectToAction(nameof(Index));
        }

        // ---------- إضافة فئة جديدة ----------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ProductRepository.AddCategory(name.Trim());
                TempData["Msg"] = $"➕ تمت إضافة الفئة «{name.Trim()}»";
            }
            return RedirectToAction(nameof(Index));
        }

        #region معالجة الصور
        private void ValidateImage(ProductInputModel model)
        {
            if (model.ImageFile is not null && model.ImageFile.Length > 0)
            {
                var ext = Path.GetExtension(model.ImageFile.FileName).ToLowerInvariant();
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                if (!allowed.Contains(ext))
                    ModelState.AddModelError(nameof(model.ImageFile),
                        "صيغة الصورة غير مدعومة — المسموح: JPG, PNG, GIF, WEBP");
                else if (model.ImageFile.Length > 5 * 1024 * 1024)
                    ModelState.AddModelError(nameof(model.ImageFile),
                        "حجم الصورة يجب ألا يتجاوز 5 ميغابايت");
            }
        }

        private async Task<string?> SaveUploadedFileAsync(IFormFile? file, string? existingUrl)
        {
            if (file is null || file.Length == 0) return existingUrl;

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDir);

            var safeName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
            var fullPath = Path.Combine(uploadsDir, safeName);

            using var stream = System.IO.File.Create(fullPath);
            await file.CopyToAsync(stream);

            return $"/uploads/{safeName}";
        }
        #endregion
    }
}
