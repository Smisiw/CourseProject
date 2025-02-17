using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.DTO.Brand
{
    public class BrandDTO
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
    }
}
