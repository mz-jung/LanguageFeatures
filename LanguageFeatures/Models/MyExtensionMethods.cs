using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> productEnum) { // this = 화장메서드로 표시해주는 역할. 매개변수 : 확장메서드가 적용될 클래스 지정.
            decimal total = 0;
            foreach (Product prod in productEnum) {
                total += prod.Price;
            }
            return total;
        }

        /// <summary>
        /// 필터링 확장 메서드 구현
        /// </summary>
        /// <param name="productEnum"></param>
        /// <param name="categoryParam"></param>
        /// <returns></returns>
        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> productEnum, string categoryParam) {
            foreach (Product prod in productEnum) {
                if (prod.Category == categoryParam) {
                    yield return prod;
                }
            }
        }
    }
}