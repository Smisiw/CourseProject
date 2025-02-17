namespace CourseProject.Data.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
