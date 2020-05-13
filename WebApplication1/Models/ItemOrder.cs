using System;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class ItemOrder

    {
        public ItemOrder()
        {
        }

        [JsonProperty("itemID")] public long itemID { get; set; }

        [JsonProperty("tableNO")] public String tableNO { get; set; }
        
        [JsonProperty("quantity")] public int quantity { get; set; }
        
        [JsonProperty("price")] public double price { get; set; }
        
        
    }

}