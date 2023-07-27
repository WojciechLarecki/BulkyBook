using BulkyBook.DataAccess.Repositories.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _db = unitOfWork;
        }

        public IActionResult Index()
        {
            var products = _db.ProductRepository.GetAll("Category, CoverType");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var shoppingCart = new ShoppingCart()
            {
                Product = _db.ProductRepository.GetFirstOrDefault(p => p.Id == id, "Category, CoverType"),
                Count = 1
            };
            return View(shoppingCart);
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
    }
}