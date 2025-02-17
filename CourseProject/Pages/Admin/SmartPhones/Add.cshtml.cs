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

namespace CourseProject.Pages.Admin.SmartPhones
{
    public class AddModel : PageModel
    {
        private readonly IBrandService _brandService;
        private readonly ISmartPhoneService _smartPhoneService;
        public AddModel(IBrandService brandService, ISmartPhoneService smartPhoneService)
        {
            _brandService = brandService;
            _smartPhoneService = smartPhoneService;
        }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        [BindProperty]
        public SmartPhoneDTO SmartPhone { get; set; }
        public void OnGet()
        {
            Brands = _brandService.GetBrands().Data;
        }
        public async Task OnPostAsync()
        {
            ModelState.Remove("SmartPhone.ImageByte");
            ModelState.Remove("SmartPhone.Image");
            if (ModelState.IsValid)
            {
                var image = Request.Form.Files["file"];
                if ( image != null && image.Length > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)image.Length);
                    }
                    var response = await _smartPhoneService.CreateSmartPhone(SmartPhone, imageData);
                }
            }
            Brands = _brandService.GetBrands().Data;
        }
    }
}
