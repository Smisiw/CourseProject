using CourseProject.Data.DTO.Phone;
using CourseProject.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.DTO.SmartPhone
{
    public class SmartPhoneDTO : PhoneDTO
    {
        [Required(ErrorMessage = "Введите количество внутренней памяти")]
        public decimal Memory { get; set; }
        [Required(ErrorMessage = "Введите количество оперативной памяти")]
        public decimal Ram { get; set; }
        [Required(ErrorMessage = "Введите название операционной системы")]
        public string OS { get; set; }
        [Required(ErrorMessage = "Введите версию операционной системы")]
        public string OSVersion { get; set; }
        [Required(ErrorMessage = "Введите количество ядер процессора")]
        public int CoreNumber { get; set; }
        [Required(ErrorMessage = "Введите частоту процессора")]
        public decimal Frequency { get; set; }
    }
}
