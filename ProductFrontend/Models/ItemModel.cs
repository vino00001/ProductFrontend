using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductFrontend.Models
{
    public class ItemModel
    {
        public string Market { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string PriceDuration { get; set; }
    }
}