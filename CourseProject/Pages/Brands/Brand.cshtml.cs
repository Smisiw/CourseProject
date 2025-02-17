using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.Phone;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Brands
{
    public class BrandModel : PageModel
    {
        private readonly IBrandService _brandService;
        private readonly IPhoneService _phoneService;
        private readonly ICartService _cartService;
        public BrandModel(IBrandService brandService, IPhoneService phoneService, ICartService cartService)
        {
            _brandService = brandService;
            _phoneService = phoneService;
            _cartService = cartService;
        }


        [BindProperty]
        public BrandDTO Brand { get; set; }
        public List<PhoneDTO> Phones { get; set; }
        public void OnGet(int id)
        {
            Brand = _brandService.GetBrand(id).Result.Data;
            Phones = _phoneService.GetPhoneByBrand(id).Result.Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            Brand = _brandService.GetBrand(id).Result.Data;
            Phones = _phoneService.GetPhoneByBrand(id).Result.Data;
            return Page();
        }
    }
}
