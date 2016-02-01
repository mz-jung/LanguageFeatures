using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public class Product
    {
        
        /// <summary>
        /// 자동으로 구현된 속성기능 사용
        /// </summary>
        /// 
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }


    }
}