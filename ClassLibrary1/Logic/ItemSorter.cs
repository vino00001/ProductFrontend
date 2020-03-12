using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Models;

namespace ClassLibrary1.Logic
{
    public static class ItemSorter
    {
        public static List<ItemModel> SortItems(List<ItemModel> items)
        {
            items = items.OrderBy(x => x.MarketId).ThenBy(x => x.CurrencyCode).ThenBy(x => x.ValidFrom).ToList();   // sorts the list

            return OptimizeItems(items);
        }

        private static List<ItemModel> OptimizeItems(List<ItemModel> items)
        {
            List<ItemModel> result = new List<ItemModel>(); // creates a new empty list to hold the result
            var groups = items.GroupBy(item => item.MarketCurreny); // creates a group for every market + currency comvbo
            foreach (var group in groups)
            {
                var standardPrice = group.First(x => x.ValidUntil == null); // ItemModel with the standard price (1970-01-01 00:00:00 - NULL)
                ItemModel previousItem = standardPrice;
                result.Add(standardPrice);
                previousItem.ValidUntil = DateTime.MaxValue;
                foreach (var item in group)
                {
                    if (previousItem == item) // skip this iteration for the first item
                    {
                        continue;
                    }

                    var timeDiff = item.ValidFrom - previousItem.ValidUntil;    // timeDiff determines if the compareded items have a gap, and overlap or follows eachother
                    if (timeDiff.Value.Ticks > 0) // gap
                    {
                        var newItem = new ItemModel // closes the gap and assigns the standard price to the UnitPrice
                        {
                            MarketId = item.MarketId,
                            CurrencyCode = item.CurrencyCode,
                            UnitPrice = standardPrice.UnitPrice,
                            ValidFrom = previousItem.ValidUntil,
                            ValidUntil = item.ValidFrom,
                        };
                        result.Add(newItem);
                        previousItem = newItem;
                    }
                    else if (timeDiff.Value.Ticks < 0) // overlap
                    {
                        if (previousItem.UnitPrice > item.UnitPrice) // if the current items price is cheaper that the previous one, merge the gap
                        {
                            previousItem.ValidUntil = item.ValidFrom;
                        }
                        else
                        {
                            if (item.ValidUntil < previousItem.ValidUntil) // continue if the previous item overlaps the current one...
                            {
                                continue;
                            }

                            previousItem.ValidFrom = item.ValidUntil;   // otherwize override the current item
                        }
                    }

                    // if there are no gaps or overlaps, continue with the following code
                    if (item.UnitPrice < standardPrice.UnitPrice) //  if the item price is better than the standard one, add the current item
                    {
                        result.Add(item);
                        previousItem = item;
                    }
                    else if (item.UnitPrice == previousItem.UnitPrice) // if the prices are the same, override the item
                    {
                        previousItem.ValidUntil = item.ValidUntil;
                    }
                    else
                    {
                        var newItem = new ItemModel // if the standard price is better, create a new item with standard price
                        {
                            MarketId = item.MarketId,
                            CurrencyCode = item.CurrencyCode,
                            UnitPrice = standardPrice.UnitPrice,
                            ValidFrom = item.ValidFrom,
                            ValidUntil = item.ValidUntil,
                        };
                        result.Add(newItem);
                        previousItem = newItem;
                    }
                }

                if (previousItem.ValidUntil != DateTime.MaxValue) // for the last item in each group, create an infinite item
                {
                    var lastItem = new ItemModel
                    {
                        MarketId = previousItem.MarketId,
                        CurrencyCode = previousItem.CurrencyCode,
                        UnitPrice = standardPrice.UnitPrice,
                        ValidFrom = previousItem.ValidUntil,
                        ValidUntil = null,
                    };
                    result.Add(lastItem);
                }
            }

            foreach (var item in result) // collapse all the items that have an infinite price, so that only one is showed
            {
                if (item.ValidUntil == DateTime.MaxValue)
                {
                    item.ValidUntil = null;
                }
            }

            return result;
        }
    }
}
