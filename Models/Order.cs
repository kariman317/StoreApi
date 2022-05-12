using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("User")]

        public string UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
