using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.CellPhone;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.CellPhones
{
    public class IndexModel : PageModel
    {
        private readonly ICellPhoneService _cellPhoneService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;
        public IndexModel(ICellPhoneService cellPhoneService, IBrandService brandService, ICartService cartService)
        {
            _cellPhoneService = cellPhoneService;
            _brandService = brandService;
            _cartService = cartService;
        }

        [BindProperty]
        public List<CellPhoneDTO> CellPhones { get; set; }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        public void OnGet()
        {
            CellPhones = _cellPhoneService.GetCellPhones().Data;
            Brands = _brandService.GetBrands().Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            CellPhones = _cellPhoneService.GetCellPhones().Data;
            Brands = _brandService.GetBrands().Data;
            return Page();
        }
    }
}
