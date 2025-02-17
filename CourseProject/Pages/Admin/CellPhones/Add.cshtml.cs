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
using CourseProject.Data.DTO.CellPhone;

namespace CourseProject.Pages.Admin.CellPhones
{
    public class AddModel : PageModel
    {
        private readonly IBrandService _brandService;
        private readonly ICellPhoneService _cellPhoneService;
        public AddModel(IBrandService brandService, ICellPhoneService cellPhoneService)
        {
            _brandService = brandService;
            _cellPhoneService = cellPhoneService;
        }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        [BindProperty]
        public CellPhoneDTO CellPhone { get; set; }
        public void OnGet()
        {
            Brands = _brandService.GetBrands().Data;
        }
        public async Task OnPostAsync()
        {
            ModelState.Remove("CellPhone.ImageByte");
            ModelState.Remove("CellPhone.Image");
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
                    var response = await _cellPhoneService.CreateCellPhone(CellPhone, imageData);
                }
            }
            Brands = _brandService.GetBrands().Data;
        }
    }
}
