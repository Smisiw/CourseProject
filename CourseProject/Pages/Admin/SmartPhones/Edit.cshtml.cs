using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Admin.SmartPhones
{
    public class EditModel : PageModel
    {
        private readonly ISmartPhoneService _smartPhoneService;
        private readonly IBrandService _brandService;
        public EditModel(ISmartPhoneService smartPhoneService, IBrandService brandService)
        {
            _smartPhoneService = smartPhoneService;
            _brandService = brandService;
        }

        [BindProperty]
        public SmartPhoneDTO SmartPhone { get; set; }
        [BindProperty]
        public List<BrandDTO> Brands { get; set; }
        public void OnGet(int id)
        {
            SmartPhone = _smartPhoneService.GetSmartPhone(id).Result.Data;
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
                SmartPhone.ImageByte = imageData;
            }
            else
            {
                SmartPhone.ImageByte = _smartPhoneService.GetSmartPhone(SmartPhone.PhoneId).Result.Data.ImageByte;
            }
            ModelState.Remove("SmartPhone.ImageByte");
            ModelState.Remove("SmartPhone.Image");
            if (ModelState.IsValid)
            {
                var response = await _smartPhoneService.UpdateSmartPhone(SmartPhone.PhoneId, SmartPhone);
            }
            Brands = _brandService.GetBrands().Data;
        }
    }
}
