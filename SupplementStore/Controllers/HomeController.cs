using Microsoft.AspNetCore.Mvc;
using SupplementStore.Models;

namespace SupplementStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BestSellers = ProductRepository.Products
                .OrderByDescending(p => p.ReviewCount)
                .Take(4)
                .ToList();
            ViewBag.Categories = ProductRepository.Categories;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
