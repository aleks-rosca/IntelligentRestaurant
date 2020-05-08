﻿using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Item

    {
        public Item()
        {
        }
        
        public int Quantity { get; set; }
        [JsonProperty("itemId")] public string Id { get; set; }

        [JsonProperty("itemName")] public string ItemName { get; set; }

        [JsonProperty("itemDesc")] public string Description { get; set; }

        [JsonProperty("itemPrice")] public decimal Price { get; set; }

        [JsonProperty("itemType")] public string Type { get; set; }
    }
}
    
    