using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data;
using CourseProject.Data.DTO.CellPhone;

namespace CourseProject.Services.Interfaces
{
    public interface ICellPhoneService
    {
        BaseResponse<List<CellPhoneDTO>> GetCellPhones();
        Task<BaseResponse<CellPhoneDTO>> GetCellPhone(int id);
        Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhone(string term);
        Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhoneByBrand(int brandId);
        Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhoneColors(int phoneId);
        Task<BaseResponse<bool>> DeleteCellPhone(int id);
        Task<BaseResponse<bool>> UpdateCellPhone(int id, CellPhoneDTO cellPhone);
        Task<BaseResponse<bool>> CreateCellPhone(CellPhoneDTO cellPhone, byte[] imageData);
    }
}
