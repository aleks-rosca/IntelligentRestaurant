using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AddToCartController : Controller
    {
        private readonly string baseUrl = "http://localhost:8080/api/";

        public ActionResult Add(Item item)
        {
            if (Session["cart"] == null)
            {
                var li = new List<Item> {item};
                Session["cart"] = li;
                ViewBag.cart = li.Count + 1;
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
        private int isExist(Item item)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ItemName.Equals(item))
                    return i;
            return -1;
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