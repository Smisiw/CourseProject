using CourseProject.Data.DTO.Brand;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.Brands
{
    public class EditModel : PageModel
    {
        private readonly IBrandService _brandService;
        public EditModel(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public BrandDTO Brand { get; set; }
        public void OnGet(int id)
        {
            Brand = _brandService.GetBrand(id).Result.Data;
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _brandService.UpdateBrand(Brand.BrandId, Brand);
            }
        }
    }
}
