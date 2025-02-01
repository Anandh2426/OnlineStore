using System.Diagnostics;
using System.Security.Claims;
using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

           IEnumerable<Product> productList = _unitOfWork.Products.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Products.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            var product = _unitOfWork.Products.Get(u => u.Id == shoppingCart.ProductId);

            if (shoppingCart.Count < 1)
            {
                TempData["ErrorMessage"] = "The count must be at least 1.";
                return RedirectToAction("Details", new { productId = shoppingCart.ProductId });
            }

            if (product == null || product.stock_quantity < shoppingCart.Count )
            {
                TempData["ErrorMessage"] = $"The product '{product?.Title}' is not available in the requested quantity.";
                return RedirectToAction("Details", new { productId = shoppingCart.ProductId });
            }

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCarts.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);
            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCarts.Update(cartFromDb);

                _unitOfWork.Save();
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCarts.Add(shoppingCart);
                _unitOfWork.Save();
                //HttpContext.Session.SetInt32(SD.SessionCart,
                //_unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Cart updated successfully";




            return RedirectToAction(nameof(Index));
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
