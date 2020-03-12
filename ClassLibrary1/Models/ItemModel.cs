using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class ItemModel
    {
        public string MarketId { get; set; }

        public string CurrencyCode { get; set; }

        public string MarketCurreny { get => MarketId + CurrencyCode; }

        public decimal UnitPrice { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
