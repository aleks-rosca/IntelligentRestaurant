using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
     
        public ActionResult SubmitOrder(ItemOrder itemOrder)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                
                var jsonString = JsonConvert.SerializeObject(itemOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                client.PostAsync("ordereditems", content);
                Console.WriteLine("We got here"+ content);
            }
            return RedirectToAction("order");        
        }
        
    }
}