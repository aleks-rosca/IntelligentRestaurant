﻿using System;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class ItemOrder

    {
        public ItemOrder()
        {
        }
            public string itemName { get; set; }
        [JsonProperty("itemID")] public int itemID { get; set; }

        [JsonProperty("tableNO")] public int tableNO { get; set; }
        
        [JsonProperty("quantity")] public int quantity { get; set; }
        
        [JsonProperty("price")] public decimal price { get; set; }
        
        
    }

}