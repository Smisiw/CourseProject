using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data;
using CourseProject.Data.DTO.Brand;

namespace CourseProject.Services.Interfaces
{
    public interface IBrandService
    {
        BaseResponse<List<BrandDTO>> GetBrands();
        Task<BaseResponse<BrandDTO>> GetBrand(int id);
        Task<BaseResponse<List<BrandDTO>>> GetBrand(string term);
        Task<BaseResponse<bool>> DeleteBrand(int id);
        Task<BaseResponse<bool>> UpdateBrand(int id, BrandDTO brand);
        Task<BaseResponse<bool>> CreateBrand(BrandDTO brand);
    }
}
