using CourseProject.Data;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Models;

namespace CourseProject.Services.Interfaces
{
    public interface ISmartPhoneService
    {
        BaseResponse<List<SmartPhoneDTO>> GetSmartPhones();
        Task<BaseResponse<SmartPhoneDTO>> GetSmartPhone(int id);
        Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhone(string term);
        Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneByBrand(int brandId);
        Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneColors(int phoneId);
        Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneMemories(int phoneId);
        Task<BaseResponse<bool>> DeleteSmartPhone(int id);
        Task<BaseResponse<bool>> UpdateSmartPhone(int id, SmartPhoneDTO smartPhone);
        Task<BaseResponse<bool>> CreateSmartPhone(SmartPhoneDTO smartPhone, byte[] imageData);
    }
}
