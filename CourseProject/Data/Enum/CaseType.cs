using System.ComponentModel.DataAnnotations;

namespace CourseProject.Data.Enum
{
    public enum CaseType
    {
        [Display(Name = "Моноблок")]
        Monoblock = 0,
        [Display(Name = "Раскладушка")]
        ClamShell = 1,
    }
}
