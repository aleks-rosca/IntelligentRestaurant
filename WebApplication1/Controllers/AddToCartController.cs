using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<ItemOrder> io = new List<ItemOrder>();
        public ActionResult Add(Item item, ItemOrder itemOrder)
        {
          
            if (Session["cart"] == null)
            {     
                var li = new List<Item> {item};
              
                Session["cart"] = li;
                Session["out"] = io;
                itemOrder.itemID = item.Id;
                itemOrder.price = item.Price;
                itemOrder.tableNO = "1";
                itemOrder.quantity = li.Count;
                ViewBag.cart = li.Count + 1;
                Session["count"] = 1;
                io.Add(itemOrder);
            }
            else
            {
                var li = (List<Item>) Session["cart"];
                io = (List<ItemOrder>) Session["out"];
                itemOrder.itemID = item.Id;
                itemOrder.price = item.Price;
                itemOrder.tableNO = "1";
                itemOrder.quantity = li.Count;
                io.Add(itemOrder);
                Console.WriteLine(io.Count);
                li.Add(item);
                Session["cart"] = li;
                Session["out"] = io;
                ViewBag.cart = li.Count;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }

            return RedirectToAction("Index", "Home",io);
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
        public async Task<ActionResult> MakeOrder()//should probably take a list of items instead of one
        {
            using (var client = new HttpClient())
            {
                
               
                client.BaseAddress = new Uri(baseUrl);
               // Console.Out.WriteLine("io = {0}", io);
                Console.WriteLine("Before sending ");
                foreach (var jsonString in io.Select(itemOrder => JsonConvert.SerializeObject(itemOrder)))
                {
                    Console.WriteLine(jsonString);
                    using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                    {
                        var response = client.PostAsync("ordereditems", content).Result;
                    }

                    Console.WriteLine("After Post ");
                }
            }

            return RedirectToAction("Index","Home"); // Where should it go after the method it done
            
        }

        public ActionResult order()// this method make the page visible when we click order 
        {
           
            //This needs to be replaced with a list of ordered items
           // ItemOrder test = new ItemOrder{itemID = "1",price = 100,quantity = 4,tableNO = "20"};

           
            return RedirectToAction("Index","Home",MakeOrder());
        }

      

        
    }
}