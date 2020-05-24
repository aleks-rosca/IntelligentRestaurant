using System.Collections;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Item
    {
        public Item()
        {
        }
        [JsonProperty("itemId")] public int Id { get; set; }

        [JsonProperty("itemName")] public string ItemName { get; set; }

        [JsonProperty("itemDesc")] public string Description { get; set; }

        [JsonProperty("itemPrice")] public decimal Price { get; set; }

        [JsonProperty("itemType")] public string Type { get; set; }

       
    }
}
    
    