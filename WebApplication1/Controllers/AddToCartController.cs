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
        // private readonly string baseUrl = "http://localhost:8080/api/";
       // private List<ItemOrder> io = new List<ItemOrder>();
        public ActionResult Add(Item newItem)
        {
            if (Session["out"] == null)
            {
                Session["out"] = new List<ItemOrder>();
            }

            var alreadyOrderedItemIndex = 0;
            bool alreadyHasItemType = false;

            try
            {
                List<ItemOrder> currentOrders = Session["out"] as List<ItemOrder>;
                if (currentOrders != null)
                {
                    for (int i = 0; i < currentOrders.Count; i++)
                    {
                        if (((List<ItemOrder>) Session["out"])[i].itemID == newItem.Id)
                        {
                            alreadyHasItemType = true;
                            alreadyOrderedItemIndex = i;
                            break;
                        }
                    }

                    if (alreadyHasItemType)
                    {
                        // already have item(s) of this type
                        ((List<ItemOrder>) Session["out"])[alreadyOrderedItemIndex].quantity++;
                    }
                    else
                    {
                        // first item of this type
                        var itemOrder = new ItemOrder
                        {
                            itemID = newItem.Id, 
                            price = newItem.Price , 
                            quantity = 1, 
                            tableNO = 1
                        };
                        (Session["out"] as List<ItemOrder>)?.Add(itemOrder);
                    }


                    ViewBag.cart = ((List<ItemOrder>) Session["out"]).Count;
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

            } catch (Exception e) {
                Console.WriteLine(e);
                throw e;
            }




            // if (Session["cart"] == null)
            // {     
            //     var li = new List<Item> {item};
            //   
            //     Session["cart"] = li;
            //     Session["out"] = io;
            //     itemOrder.itemID = item.Id;
            //     itemOrder.price = item.Price;
            //     itemOrder.tableNO = "1";
            //     itemOrder.quantity = li.Count;
            //     ViewBag.cart = li.Count + 1;
            //     Session["count"] = 1;
            //     io.Add(itemOrder);
            // }
            // else
            // {
            //     var li = (List<Item>) Session["cart"];
            //     io = (List<ItemOrder>) Session["out"];
            //     itemOrder.itemID = item.Id;
            //     itemOrder.price = item.Price;
            //     itemOrder.tableNO = "1";
            //     itemOrder.quantity = li.Count;
            //     io.Add(itemOrder);
            //     Console.WriteLine(io.Count);
            //     li.Add(item);
            //     io.Add(itemOrder);
            //     Session["cart"] = li;
            //     Session["out"] = io;
            //     ViewBag.cart = li.Count;
            //     Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            // }
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> AddToCart()
        {
            return await new SubmitOrderController().SubmitOrder(((List<ItemOrder>) Session["out"]));
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
            return View((List<ItemOrder>) Session["out"]);
        }

        public ActionResult Remove(ItemOrder item)
        {
            var li = (List<ItemOrder>) Session["out"];
            li.RemoveAll(x => x.itemID == item.itemID);
            Session["out"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");
        }
        //
        // [HttpPost]
        // [ActionName("order")]
        // public async Task<ActionResult> MakeOrder()//should probably take a list of items instead of one
        // {
        //     using (var client = new HttpClient())
        //     {
        //         
        //        
        //         client.BaseAddress = new Uri(baseUrl);
        //        // Console.Out.WriteLine("io = {0}", io);
        //         Console.WriteLine("Before sending ");
        //         foreach (var jsonString in io.Select(itemOrder => JsonConvert.SerializeObject(itemOrder)))
        //         {
        //             Console.WriteLine(jsonString);
        //             using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
        //             {
        //                 var response = client.PostAsync("ordereditems", content).Result;
        //             }
        //
        //             Console.WriteLine("After Post ");
        //         }
        //     }
        //
        //     return RedirectToAction("Index","Home"); // Where should it go after the method it done
        //     
        // }

        // public ActionResult order()// this method make the page visible when we click order 
        // {
        //    
        //     //This needs to be replaced with a list of ordered items
        //    // ItemOrder test = new ItemOrder{itemID = "1",price = 100,quantity = 4,tableNO = "20"};
        //
        //    
        //     return RedirectToAction("Index","Home",MakeOrder());
        // }

      

        
    }
}