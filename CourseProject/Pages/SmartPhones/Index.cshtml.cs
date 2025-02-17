using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.SmartPhones
{
    public class IndexModel : PageModel
    {
        private readonly ISmartPhoneService _smartPhoneService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;
        public IndexModel(ISmartPhoneService smartPhoneService, IBrandService brandService, ICartService cartService)
        {
            _smartPhoneService = smartPhoneService;
            _brandService = brandService;
            _cartService = cartService;
        }

        [BindProperty]
        public List<SmartPhoneDTO> SmartPhones { get; set; }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        public void OnGet()
        {
            SmartPhones = _smartPhoneService.GetSmartPhones().Data;
            Brands = _brandService.GetBrands().Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            SmartPhones = _smartPhoneService.GetSmartPhones().Data;
            Brands = _brandService.GetBrands().Data;
            return Page();
        }
    }
}
