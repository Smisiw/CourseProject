using CourseProject.Data.DTO.Phone;

namespace CourseProject.Data.DTO.Order
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int PhoneId { get; set; }
        public PhoneDTO Phone { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
