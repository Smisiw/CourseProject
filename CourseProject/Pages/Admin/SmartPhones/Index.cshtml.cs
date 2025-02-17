using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.SmartPhones
{
    public class IndexModel : PageModel
    {
        private readonly ISmartPhoneService _smartPhoneService;
        public IndexModel(ISmartPhoneService smartPhoneService)
        {
            _smartPhoneService = smartPhoneService;
        }
        [BindProperty]
        public SmartPhoneDTO smartPhoneDTO { get; set; }
        [BindProperty]
        public List<SmartPhoneDTO> smartPhones { get; set; }

        public void OnGet()
        {
            smartPhones = _smartPhoneService.GetSmartPhones().Data;
        }

        public async Task OnPostDeleteAsync(int phoneId)
        {
            var response = _smartPhoneService.DeleteSmartPhone(phoneId);
            smartPhones = _smartPhoneService.GetSmartPhones().Data;
        }
    }
}
