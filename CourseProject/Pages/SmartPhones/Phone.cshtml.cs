using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.SmartPhones
{
    public class PhoneModel : PageModel
    {
        private readonly ISmartPhoneService _smartPhoneService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;
        public PhoneModel(ISmartPhoneService smartPhoneService, IBrandService brandService, ICartService cartService)
        {
            _smartPhoneService = smartPhoneService;
            _brandService = brandService;
            _cartService = cartService;
        }

        [BindProperty]
        public SmartPhoneDTO SmartPhone { get; set; }
        [BindProperty]
        public BrandDTO Brand { get; set; }
        public List<SmartPhoneDTO> SmartPhoneColors { get; set; }
        public List<SmartPhoneDTO> SmartPhoneMemories { get; set; }
        public void OnGet(int id)
        {
            SmartPhone = _smartPhoneService.GetSmartPhone(id).Result.Data;
            Brand = _brandService.GetBrand(SmartPhone.BrandId).Result.Data;
            SmartPhoneColors = _smartPhoneService.GetSmartPhoneColors(id).Result.Data;
            SmartPhoneMemories = _smartPhoneService.GetSmartPhoneMemories(id).Result.Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            SmartPhone = _smartPhoneService.GetSmartPhone(id).Result.Data;
            Brand = _brandService.GetBrand(SmartPhone.BrandId).Result.Data;
            SmartPhoneColors = _smartPhoneService.GetSmartPhoneColors(id).Result.Data;
            SmartPhoneMemories = _smartPhoneService.GetSmartPhoneMemories(id).Result.Data;
            return Page();
        }
    }
}
