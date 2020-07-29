using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class ItemOrder

    {
        [JsonProperty("tableNO")] public int tableNO = 2;
        [JsonProperty("itemID")] public int itemID { get; set; }

        [Display(Name = "Item Name")]
        [JsonProperty("itemName")]
        public string itemName { get; set; }

        [Display(Name = "Quantity")]
        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [Display(Name = "Price")]
        [JsonProperty("price")]
        public decimal price { get; set; }
    }
}