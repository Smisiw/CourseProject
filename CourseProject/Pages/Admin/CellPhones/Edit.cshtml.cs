using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.CellPhone;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.CellPhones
{
    public class EditModel : PageModel
    {
        private readonly ICellPhoneService _cellPhoneService;
        private readonly IBrandService _brandService;
        public EditModel(ICellPhoneService cellPhoneService, IBrandService brandService)
        {
            _brandService = brandService;
            _cellPhoneService = cellPhoneService;
        }

        [BindProperty]
        public CellPhoneDTO CellPhone { get; set; }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        public void OnGet(int id)
        {
            CellPhone = _cellPhoneService.GetCellPhone(id).Result.Data;
            Brands = _brandService.GetBrands().Data;
        }

        public async Task OnPostAsync()
        {
            var image = Request.Form.Files["file"];
            if (image != null && image.Length > 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }
                CellPhone.ImageByte = imageData;
            }
            else
            {
                CellPhone.ImageByte = _cellPhoneService.GetCellPhone(CellPhone.PhoneId).Result.Data.ImageByte;
            }
            ModelState.Remove("CellPhone.ImageByte");
            ModelState.Remove("CellPhone.Image");
            if (ModelState.IsValid)
            {
                var response = await _cellPhoneService.UpdateCellPhone(CellPhone.PhoneId, CellPhone);
            }
            Brands = _brandService.GetBrands().Data;
        }
    }
}