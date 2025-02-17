using CourseProject.Data;
using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.Phone;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPhoneService _phoneService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;

        public IndexModel(ILogger<IndexModel> logger, IPhoneService phoneService, IBrandService brandService, ICartService cartService)
        {
            _logger = logger;
            _phoneService = phoneService;
            _brandService = brandService;
            _cartService = cartService;
        }

        public List<PhoneDTO> Phones { get; set; }
        public List<BrandDTO> Brands { get; set; }
        [BindProperty]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Phones = _phoneService.GetPhones().Data;
            Brands = _brandService.GetBrands().Data;
        }

        public void OnPost()
        {
            Phones = _phoneService.GetPhone(SearchTerm).Result.Data;
            Brands = _brandService.GetBrands().Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                 return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            Phones = _phoneService.GetPhone(SearchTerm).Result.Data;
            Brands = _brandService.GetBrands().Data;
            return Page();
        }
    }
}
