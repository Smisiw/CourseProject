using CourseProject.Data.DTO.CellPhone;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.CellPhones
{
    public class IndexModel : PageModel
    {
        private readonly ICellPhoneService _cellPhoneService;
        public IndexModel(ICellPhoneService cellPhoneService)
        {
            _cellPhoneService = cellPhoneService;
        }
        [BindProperty]
        public List<CellPhoneDTO> CellPhones { get; set; }

        public void OnGet()
        {
            CellPhones = _cellPhoneService.GetCellPhones().Data;
        }

        public async Task OnPostDeleteAsync(int phoneId)
        {
            var response = _cellPhoneService.DeleteCellPhone(phoneId);
            CellPhones = _cellPhoneService.GetCellPhones().Data;
        }
    }
}
