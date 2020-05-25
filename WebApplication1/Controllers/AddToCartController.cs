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
                            itemName = newItem.ItemName,
                            itemID = newItem.Id, 
                            price = newItem.Price , 
                            quantity = 1, 
                            tableNO = 189
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

      

        
    }
}