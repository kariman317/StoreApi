using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreAPI.Models
{
    public class Product
    {
   
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
