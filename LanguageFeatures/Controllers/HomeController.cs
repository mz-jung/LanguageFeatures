using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty() {
            Product myProduct = new Product();

            myProduct.Name = "kim";

            string productName = myProduct.Name;

            return View("Result", (object)String.Format("Product name : {0}", productName));
        }

        public ViewResult CreateProduct() {
            // 개체 이니셜라이저 기능 
            Product myProduct = new Product
            {
                ProductId = 1,
                Name = "Lee",
                Description = "hi~",
                Price = 200M,
                Category = "waterSports"
            };

            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection() {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };

            Dictionary<string, int> myDict = new Dictionary<string, int> {
                {"apple",10 } , {"orange", 20}, {"plum", 30 }
            };

            return View("Result", (object)stringArray[1]);
        }

        /// <summary>
        /// 확장메서드 사용!
        /// </summary>
        /// <returns></returns>
        public ViewResult UseExtension() {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "kim", Price = 200M },
                    new Product {Name = "Lee", Price = 200M },
                    new Product {Name = "Park", Price = 200M },
                    new Product {Name = "Han", Price = 200M }
                }
            };

            decimal cartTotal = cart.TotalPrices();

            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }
    }
}