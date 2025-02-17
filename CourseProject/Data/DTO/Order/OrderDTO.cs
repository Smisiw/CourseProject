using CourseProject.Data.Models;

namespace CourseProject.Data.DTO.Order
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public decimal TotalPrice => OrderItems.Sum(x => x.Price * x.Count);
    }
}
