using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// 인터페이스에 확장메서드 적용
        /// </summary>
        /// <returns></returns>
        public ViewResult UseExtensionEnumerable() {

            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "kim", Price = 200M },
                    new Product {Name = "Lee", Price = 200M },
                    new Product {Name = "Park", Price = 200M },
                    new Product {Name = "Han", Price = 200M }
                }
            };

            Product[] productArray = {
                new Product {Name = "kim", Price = 200M },
                new Product {Name = "Lee", Price = 200M },
                new Product {Name = "Park", Price = 200M },
                new Product {Name = "Han", Price = 200M }
            };

            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        /// <summary>
        /// 필터링 확장 메서드 사용
        /// </summary>
        /// <returns></returns>
        public ViewResult UseFilterExtensionMethod() {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "kim", Price = 100M, Category = "A" },
                    new Product {Name = "Lee", Price = 200M , Category = "B"},
                    new Product {Name = "Park", Price = 300M , Category = "A"},
                    new Product {Name = "Han", Price = 400M , Category = "C" }
                }
            };

            //Func<Product, bool> categoryFilter = delegate (Product prod)
            //{
            //    return prod.Category == "A";
            //};

            //Func<Product, bool> categoryFilter = prod => prod.Category == "A";

            decimal total = 0;
            //foreach (Product prod in products.FilterByCategory("A")) {
            //foreach (Product prod in products.Filter(categoryFilter))
            foreach (Product prod in products.Filter(prod => prod.Category == "A"))
            {
                total += prod.Price;
            }

            return View("Result", (object)String.Format("Total : {0}", total));
        }

        /// <summary>
        /// 익명형식 사용
        /// </summary>
        /// <returns></returns>
        public ViewResult CreateAnonArray() {
            var oddsAndEnds = new[]{
                new {Name = "kim", Category = "A" },
                new {Name = "kim1", Category = "B" },
                new {Name = "kim2", Category = "C" },
                new {Name = "kim3", Category = "A" }
            };

            StringBuilder result = new StringBuilder();
            foreach(var item in oddsAndEnds) {
                result.Append(item.Name).Append(" ");
            }

            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts() {

            Product[] products = {
                new Product {Name = "kim1", Category = "A", Price = 200M },
                new Product {Name = "kim2", Category = "B", Price = 300M },
                new Product {Name = "kim3", Category = "C", Price = 400M },
                new Product {Name = "kim4", Category = "A", Price = 500M }
            };

            // ======== linq 미사용
            //Product[] foundProducts = new Product[3];

            //Array.Sort(products, (item1, item2) =>
            //{
            //    return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            //});

            //Array.Copy(products, foundProducts, 3);

            //StringBuilder result = new StringBuilder();
            //foreach (Product p in foundProducts)
            //{
            //    result.AppendFormat("Price: {0}", p.Price);
            //}

            // ======== linq 사용
            //var foundProducts = from match in products
            //                    orderby match.Price descending
            //                    select new { match.Name, match.Price };

            //int count = 0;
            //StringBuilder result = new StringBuilder();
            //foreach (var p in foundProducts)
            //{
            //    result.AppendFormat("Price : {0} ", p.Price);
            //    if (++count == 3)
            //    {
            //        break;
            //    }
            //}


            // ======== linq 사용 (마침표 기법)
            var foundProducts = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });

            // 지연되는 Linq 확장 메서드
            products[2] = new Product { Name = "Lee", Price = 700M }; //linq 사용으로 인해 다시 질의 수행. 결과 변경.

            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price : {0} ", p.Price);
            }

            return View("Result", (object)result.ToString());
        }

        public ViewResult SumProducts() {
            Product[] products = {
                new Product {Name = "kim1", Category = "A", Price = 200M },
                new Product {Name = "kim2", Category = "B", Price = 300M },
                new Product {Name = "kim3", Category = "C", Price = 400M },
                new Product {Name = "kim4", Category = "A", Price = 500M }
            };

            var results = products.Sum(e => e.Price); //즉시 수행되는 linq질의

            products[2] = new Product { Name = "lee", Price = 800M };

            return View("Result", (object)String.Format("Sum : {0:c}", results));
           
        }


    }
}