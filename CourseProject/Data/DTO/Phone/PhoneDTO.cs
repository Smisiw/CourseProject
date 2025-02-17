using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.DTO.Phone
{
    public class PhoneDTO
    {
        public int PhoneId { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выберите бренд")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Введите цвет")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Введите Разрешение камеры")]
        public decimal Camera { get; set; }
        [Required(ErrorMessage = "Введите емкость аккумулятора")]
        public decimal Capacity { get; set; }
        [Required(ErrorMessage = "Введите длину")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Введите ширину")]
        public decimal Width { get; set; }
        [Required(ErrorMessage = "Введите толщину")]
        public decimal Thickness { get; set; }
        [Required(ErrorMessage = "Введите вес")]
        public decimal Weigth { get; set; }
        [Required(ErrorMessage = "Выберите изображение")]
        public IFormFile Image { get; set; }
        public byte[] ImageByte { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Введите количество")]
        public int Count { get; set; }
    }
}
