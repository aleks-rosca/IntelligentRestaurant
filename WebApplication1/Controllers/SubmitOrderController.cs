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
    public class SubmitOrderController : Controller
    {
        private readonly string baseUrl = "http://localhost:8080/api/";

        [HttpPost]
        [ActionName("order")]
        public async Task<ActionResult> SubmitOrder(List<ItemOrder> orderItems)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var jsonString = JsonConvert.SerializeObject(orderItems);
                Console.WriteLine("jsonString: " + jsonString);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("ordereditems", content);
                Console.WriteLine("RESPONSE success: " + response.IsSuccessStatusCode);
            }
            

            return RedirectToAction("emptyCart", "AddToCart");
            
        }

        [HttpGet]
        [ActionName("order")]
        public ActionResult order(ItemOrder itemOrder)
        {
            Console.WriteLine(itemOrder.itemID);
            
            return View();
        }
    }
}