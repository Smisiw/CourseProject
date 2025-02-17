using CourseProject.Data.Enum;

namespace CourseProject.Data.Models
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Color { get; set; }
        public decimal Camera { get; set; }
        public decimal Capacity { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Thickness { get; set; }
        public decimal Weigth { get; set; }
        public byte[]? Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
