using CourseProject.Data.DTO.Brand;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Brands
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
    }
}
