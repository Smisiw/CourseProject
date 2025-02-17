using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CourseProject.Pages.Admin.Brands
{
    public class AddModel : PageModel
    {
        private readonly IBrandService _brandService;
        public AddModel(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [BindProperty]
        public BrandDTO Brand { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _brandService.CreateBrand(Brand);
            }
            return Page();
        }
    }
}
