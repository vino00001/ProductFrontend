using System.Collections.Generic;
using ClassLibrary.Data;
using ClassLibrary.Models;
using ClassLibrary1.Logic;

namespace ClassLibrary.Logic
{
    public static class ItemHandler
    {
        public static List<ItemModel> LoadItems(string skuId)
        {
            string sql = @"SELECT MarketId, CurrencyCode, UnitPrice, ValidFrom, ValidUntil
                           FROM csv WHERE CatalogEntryCode = @skuId";
            List<ItemModel> items = DataAccess.GetData<ItemModel>(sql, skuId);
            return ItemSorter.SortItems(items);
        }
    }
}
