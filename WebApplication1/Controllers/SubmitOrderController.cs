using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Web.Services3.Referral;
using Newtonsoft.Json;
using NuGet;
using WebApplication1.Models;
using HttpClient = System.Net.Http.HttpClient;

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

            return RedirectToAction("Index", "Home");
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