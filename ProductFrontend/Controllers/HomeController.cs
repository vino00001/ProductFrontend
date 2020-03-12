using ClassLibrary.Logic;
using ProductFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductFrontend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sku(string id)
        {
            ViewBag.Message = "View SKU";

            var data = ItemHandler.LoadItems(id);
            List<ItemModel> items = new List<ItemModel>();

            foreach (var row in data)    //  convert from ClassLibrary ItemModel to ProductFrontend ItemModel
            {
                items.Add(new ItemModel
                {
                    Market = row.MarketId,
                    Currency = row.CurrencyCode,
                    Price =  Math.Round(row.UnitPrice, 2),
                    PriceDuration = $"{row.ValidFrom} - {row.ValidUntil}"
                });
            }

            return View(items);
        }
    }
}