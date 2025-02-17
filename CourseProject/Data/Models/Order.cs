namespace CourseProject.Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
