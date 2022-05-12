using System.ComponentModel.DataAnnotations.Schema;

namespace StoreAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
