using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data;
using CourseProject.Data.DTO.Phone;

namespace CourseProject.Services.Interfaces
{
    public interface IPhoneService
    {
        BaseResponse<List<PhoneDTO>> GetPhones();
        Task<BaseResponse<PhoneDTO>> GetPhone(int id);
        Task<BaseResponse<List<PhoneDTO>>> GetPhone(string term);
        Task<BaseResponse<List<PhoneDTO>>> GetPhoneByBrand(int brandId);
    }
}
