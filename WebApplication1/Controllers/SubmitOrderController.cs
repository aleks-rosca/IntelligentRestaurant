using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SubmitOrderController : Controller
    {
        private readonly string baseUrl = "http://localhost:8080/api/";

        [ActionName("order")]
        public ActionResult SubmitOrder(ItemOrder itemOrder)
        {
            IEnumerable<ItemOrder> itemOrders = new List<ItemOrder>();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                
                var jsonString = JsonConvert.SerializeObject(itemOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
             //   client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "ordereditems"));
                client.PostAsync("ordereditems", content);
                Console.WriteLine("We got here"+ content);
            }
            return View(Session["out"]);        }
    }
}