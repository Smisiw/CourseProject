using CourseProject.Data.DTO.Phone;
using CourseProject.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.DTO.CellPhone
{
    public class CellPhoneDTO : PhoneDTO
    {
        [Required(ErrorMessage = "Выберите тип корпуса")]
        public string CaseType { get; set; }
    }
}
