using CourseProject.Data.DTO.Phone;
using CourseProject.Data.Models;

namespace CourseProject.Data.DTO.CartItem
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int PhoneId { get; set; }
        public PhoneDTO Phone { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
