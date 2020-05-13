using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers 
{
    public class sendOrderController : Controller
    {
        private readonly string baseUrl = "http://localhost:8080/api/";
        
        [HttpPost]
        [ActionName("Myorder")]
        public async Task<ActionResult> sendToOrder(ItemOrder itemOrder)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                
                var jsonString = JsonConvert.SerializeObject(itemOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                await client.PostAsync("ordereditems", content);
            }
            return RedirectToAction("Myorder");
        }

        public ActionResult Myorder()
        {
            return View();
        }

        
        
    }
}