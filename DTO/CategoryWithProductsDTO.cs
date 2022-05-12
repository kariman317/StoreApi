using System.Collections.Generic;

namespace StoreAPI.DTO
{
    public class CategoryWithProductsDTO
    {
        public int CateogryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductDetailsDTO> ProductList { get; set; } = new List<ProductDetailsDTO>();
    }
}
