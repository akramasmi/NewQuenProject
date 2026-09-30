using Microsoft.AspNetCore.Mvc;
using SupplementStore.Models;

namespace SupplementStore.Controllers
{
    public class ShopController : Controller
    {
        // قائمة المنتجات مع التصفية والبحث
        public IActionResult Index(string? category, string? query)
        {
            ViewBag.CurrentCategory = category;
            ViewBag.Query = query;
            ViewBag.Categories = ProductRepository.Categories;
            var products = ProductRepository.Search(category, query);
            return View(products);
        }

        // صفحة تفاصيل المنتج
        public IActionResult Details(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product == null)
                return NotFound();

            ViewBag.Related = ProductRepository.Products
                .Where(p => p.Category == product.Category && p.Id != product.Id)
                .Take(3)
                .ToList();

            return View(product);
        }
    }
}
