using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.CellPhone;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.CellPhones
{
    public class PhoneModel : PageModel
    {
        private readonly ICellPhoneService _cellPhoneService;
        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;
        public PhoneModel(ICellPhoneService cellPhoneService, IBrandService brandService, ICartService cartService)
        {
            _cellPhoneService = cellPhoneService;
            _brandService = brandService;
            _cartService = cartService;
        }

        [BindProperty]
        public CellPhoneDTO CellPhone { get; set; }
        [BindProperty]
        public BrandDTO Brand { get; set; }
        public List<CellPhoneDTO> CellPhoneColors { get; set; }
        public void OnGet(int id)
        {
            CellPhone = _cellPhoneService.GetCellPhone(id).Result.Data;
            Brand = _brandService.GetBrand(CellPhone.BrandId).Result.Data;
            CellPhoneColors = _cellPhoneService.GetCellPhoneColors(id).Result.Data;
        }

        public async Task<IActionResult> OnPostCartAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            await _cartService.AddItemToCart(User.Identity.Name, id);
            CellPhone = _cellPhoneService.GetCellPhone(id).Result.Data;
            Brand = _brandService.GetBrand(CellPhone.BrandId).Result.Data;
            CellPhoneColors = _cellPhoneService.GetCellPhoneColors(id).Result.Data;
            return Page();
        }
    }
}
