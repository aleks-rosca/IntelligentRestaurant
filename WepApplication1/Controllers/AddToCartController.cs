using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Core.Internal;
using CMS.Base;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AddToCartController : Controller
    {
        public ActionResult Add(Item newItem)
        {
            if (Session["out"] == null) Session["out"] = new List<ItemOrder>();

            var alreadyOrderedItemIndex = 0;
            var alreadyHasItemType = false;

            try
            {
                var currentOrders = Session["out"] as List<ItemOrder>;
                if (currentOrders != null)
                {
                    for (var i = 0; i < currentOrders.Count; i++)
                        if (((List<ItemOrder>) Session["out"])[i].itemID == newItem.Id)
                        {
                            alreadyHasItemType = true;
                            alreadyOrderedItemIndex = i;
                            break;
                        }

                    if (alreadyHasItemType)
                    {
                        ((List<ItemOrder>) Session["out"])[alreadyOrderedItemIndex].quantity++;
                    }
                    else
                    {
                        var itemOrder = new ItemOrder
                        {
                            itemName = newItem.ItemName,
                            itemID = newItem.Id,
                            price = newItem.Price,
                            quantity = 1
                        };
                        (Session["out"] as List<ItemOrder>)?.Add(itemOrder);
                        var c = itemOrder.quantity.ToInteger(int.MaxValue);
                    }

                    ViewBag.cart = ((List<ItemOrder>) Session["out"]).Count;

                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> AddToCart()
        {
            return await new SubmitOrderController().SubmitOrder((List<ItemOrder>) Session["out"]);
        }

        public ActionResult Myorder()
        {
            if (Session.IsNullOrEmpty()) return RedirectToAction("Index", "Home");

            return View((List<ItemOrder>) Session["out"]);
        }

        public ActionResult Remove(ItemOrder item)
        {
            var li = (List<ItemOrder>) Session["out"];

            li.RemoveAll(x => x.itemID == item.itemID);
            var c = item.quantity;
            Console.WriteLine(c);
            Console.WriteLine(li.Count + "--");
            Session["out"] = li;
            if (li.Count == 0)
                Session["count"] = 0;
            else if (c > 1)
                Session["count"] = li.Count + 1;
            else
                Session["count"] = li.Count;
            return RedirectToAction("Myorder", "AddToCart");
        }

        public ActionResult emptyCart()
        {
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }
    }
}