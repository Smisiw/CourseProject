using CourseProject.Data;
using CourseProject.Data.DTO.Brand;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Enum;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Data.Repositories;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBaseRepository<Brand> _brandRepository;
        public BrandService(IBaseRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> CreateBrand(BrandDTO brand)
        {
            try
            {
                var data = new Brand()
                {
                    Name = brand.Name
                };
                await _brandRepository.CreateAsync(data);
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>()
                {
                    Description = $"[CreateBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> DeleteBrand(int id)
        {
            try
            {
                var isDeleted = await _brandRepository.DeleteAsync(id);
                if (isDeleted)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.OK,
                        Data = true
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.InternalServerError,
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<BrandDTO>> GetBrand(int id)
        {
            try
            {
                var brand = await _brandRepository.GetAll().FirstOrDefaultAsync(x => x.BrandId == id);
                if (brand == null)
                {
                    return new BaseResponse<BrandDTO>()
                    {
                        Description = "Бренд не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                var data = new BrandDTO
                {
                    BrandId = brand.BrandId,
                    Name = brand.Name,
                };
                return new BaseResponse<BrandDTO>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BrandDTO>()
                {
                    Description = $"[GetBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<BrandDTO>>> GetBrand(string term)
        {
            try
            {
                var data = await _brandRepository.GetAll().Select(brand => new BrandDTO
                {
                    BrandId = brand.BrandId,
                    Name = brand.Name,
                }).Where(x => EF.Functions.Like(x.Name, $"%{term}%")).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<BrandDTO>>()
                    {
                        Description = "Найдено 0 брендов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<BrandDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<BrandDTO>>()
                {
                    Description = $"[GetBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<List<BrandDTO>> GetBrands()
        {
            try
            {
                var data = _brandRepository.GetAll().Select(brand => new BrandDTO
                {
                    BrandId = brand.BrandId,
                    Name = brand.Name,
                }).ToList();
                if (!data.Any())
                {
                    return new BaseResponse<List<BrandDTO>>()
                    {
                        Description = "Найдено 0 брендов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<BrandDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<BrandDTO>>()
                {
                    Description = $"[GetBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> UpdateBrand(int id, BrandDTO brand)
        {
            try
            {
                var data = await _brandRepository.GetAll().FirstOrDefaultAsync(x => x.BrandId == id);
                if (data == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Бренд не найден",
                        StatusCode = StatusCode.InternalServerError,
                        Data = false
                    };
                }
                data.BrandId = brand.BrandId;
                data.Name = brand.Name;
                var isDeleted = _brandRepository.Update(data);
                if (isDeleted)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.OK,
                        Data = true
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.InternalServerError,
                        Description = "Бренд не удален",
                        Data = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateBrand] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
    }
}
