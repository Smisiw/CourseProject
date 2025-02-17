namespace CourseProject.Data.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
