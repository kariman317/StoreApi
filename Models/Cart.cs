using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("User")]

        public string UserId { get; set; }
        public virtual User User { get; set; }
        [JsonIgnore]
        public ICollection<CartItem> CartItems { get; set; }
    }
}
