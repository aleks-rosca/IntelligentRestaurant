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

        public ActionResult Add(Item item, ItemOrder itemOrder)
        {
            if (Session["cart"] == null)
            {
                var li = new List<Item> {item};
                var io = new List<ItemOrder> {itemOrder};
                Session["cart"] = li;
                Session["out"] = io;
                itemOrder.itemID = item.Id;
                itemOrder.price = item.Price;
                itemOrder.tableNO = "1";
                itemOrder.quantity = li.Count;
                ViewBag.cart = li.Count + 1;
                Session["count"] = 1;
            }
            else
            {
                var li = (List<Item>) Session["cart"];
                var io = (List<ItemOrder>) Session["out"];
                itemOrder.itemID = item.Id;
                itemOrder.price = item.Price;
                itemOrder.tableNO = "1";
                itemOrder.quantity = li.Count;
                io.Add(itemOrder);
                li.Add(item);
                Session["cart"] = li;
                ViewBag.cart = li.Count;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }

            return RedirectToAction("Index", "Home");
        }

        private int isExist(Item item)
        {
            var cart = (List<Item>) Session["cart"];
            for (var i = 0; i < cart.Count; i++)
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

        [HttpPost]
        [ActionName("order")]
        public async Task<ActionResult> MakeOrder(ItemOrder itemOrder)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                Console.WriteLine("Before sending ");

                var jsonString = JsonConvert.SerializeObject(itemOrder);
                Console.WriteLine(jsonString);
                using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                {
                    var response= client.PostAsync("ordereditems", content);
                }

                Console.WriteLine("After Post "); 
            }

            return RedirectToAction("Index","Home");
            
        }

        public ActionResult order()
        {
            ItemOrder test = new ItemOrder{itemID = "1",price = 100,quantity = 4,tableNO = "19"};

           
            return RedirectToAction("Index","Home",MakeOrder(test));
        }
    }
}