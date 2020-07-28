using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class ItemOrder

    {
        [JsonProperty("itemID")] public int itemID { get; set; }
        [JsonProperty("itemName")]public string itemName { get; set; }
        
        [JsonProperty("tableNO")] public int tableNO { get; set; }

        [JsonProperty("quantity")] public int quantity { get; set; }

        [JsonProperty("price")] public decimal price { get; set; }
    }
}