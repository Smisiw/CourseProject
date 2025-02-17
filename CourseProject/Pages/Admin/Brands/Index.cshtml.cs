using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.Brands
{
    public class IndexModel : PageModel
    {
        private readonly IBrandService _brandService;
        public IndexModel(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }

        public void OnGet()
        {
            Brands = _brandService.GetBrands().Data;
        }
        public async Task OnPostDeleteAsync(int brandId)
        {
            var response = await _brandService.DeleteBrand(brandId);
            Brands = _brandService.GetBrands().Data;
        }
    }
}
