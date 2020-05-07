using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AddToCartController : Controller
    {
        public ActionResult Add(Item item)
        {
            if (Session["cart"] == null)
            {
                var li = new List<Item> {item};

                Session["cart"] = li;
                ViewBag.cart = li.Count;
                Session["count"] = 1;
            }
            else
            {
                var li = (List<Item>) Session["cart"];
                li.Add(item);
                Session["cart"] = li;
                ViewBag.cart = li.Count;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Myorder()
        {
            return View((List<Item>) Session["cart"]);
        }

        public ActionResult Remove(Item item)
        {
            var li = (List<Item>) Session["cart"];
            li.RemoveAll(x => x.ItemName == item.ItemName);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");
        }
    }
}