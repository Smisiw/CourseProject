using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Enum;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Data;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CourseProject.Data.DTO.CellPhone;

namespace CourseProject.Services.Implementations
{
    public class CellPhoneService : ICellPhoneService
    {
        private readonly IBaseRepository<CellPhone> _cellPhoneRepository;
        public CellPhoneService(IBaseRepository<CellPhone> cellPhoneRepository)
        {
            _cellPhoneRepository = cellPhoneRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> CreateCellPhone(CellPhoneDTO cellPhone, byte[] imageData)
        {
            try
            {
                var data = new CellPhone()
                {
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    Image = imageData,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = (CaseType)Convert.ToInt32(cellPhone.CaseType),
                    Description = cellPhone.Description,
                };
                await _cellPhoneRepository.CreateAsync(data);
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
                    Description = $"[CreateCellPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> DeleteCellPhone(int id)
        {
            try
            {
                var isDeleted = await _cellPhoneRepository.DeleteAsync(id);
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
                    Description = $"[DeleteCellPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<CellPhoneDTO>> GetCellPhone(int id)
        {
            try
            {
                var cellPhone = await _cellPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == id);
                if (cellPhone == null)
                {
                    return new BaseResponse<CellPhoneDTO>()
                    {
                        Description = "Телефон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                var data = new CellPhoneDTO
                {
                    PhoneId = cellPhone.PhoneId,
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    ImageByte = cellPhone.Image,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = cellPhone.CaseType.GetDisplayName(),
                    Description = cellPhone.Description,
                };
                return new BaseResponse<CellPhoneDTO>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CellPhoneDTO>()
                {
                    Description = $"[GetCellPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhone(string term)
        {
            try
            {
                var data = await _cellPhoneRepository.GetAll().Select(cellPhone => new CellPhoneDTO
                {
                    PhoneId = cellPhone.PhoneId,
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    ImageByte = cellPhone.Image,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = cellPhone.CaseType.GetDisplayName(),
                    Description = cellPhone.Description,
                }).Where(x => EF.Functions.Like(x.Name, $"%{term}%")).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<CellPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    Description = $"[GetCellPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhoneByBrand(int brandId)
        {
            try
            {
                var data = await _cellPhoneRepository.GetAll().Select(cellPhone => new CellPhoneDTO
                {
                    PhoneId = cellPhone.PhoneId,
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    ImageByte = cellPhone.Image,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = cellPhone.CaseType.GetDisplayName(),
                    Description = cellPhone.Description,
                }).Where(x => x.BrandId == brandId).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<CellPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<CellPhoneDTO>>> GetCellPhoneColors(int phoneId)
        {
            try
            {
                var phone = await _cellPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == phoneId);
                if (phone == null)
                {
                    return new BaseResponse<List<CellPhoneDTO>>()
                    {
                        Description = "Телефон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                var sortedPhones = _cellPhoneRepository.GetAll().Select(cellPhone => new CellPhoneDTO
                {
                    PhoneId = cellPhone.PhoneId,
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    ImageByte = cellPhone.Image,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = cellPhone.CaseType.GetDisplayName(),
                    Description = cellPhone.Description,
                }).Where(x => x.Name == phone.Name).OrderByDescending(x => x.Color).ToList();
                var data = new List<CellPhoneDTO>();
                foreach (var cellPhone in sortedPhones)
                {
                    if (!data.Any(x => x.Color == cellPhone.Color))
                    {
                        data.Add(cellPhone);
                    }
                }
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    Description = $"[GetCellPhoneColors] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public BaseResponse<List<CellPhoneDTO>> GetCellPhones()
        {
            try
            {
                var data = _cellPhoneRepository.GetAll().Select(cellPhone => new CellPhoneDTO
                {
                    PhoneId = cellPhone.PhoneId,
                    BrandId = cellPhone.BrandId,
                    Camera = cellPhone.Camera,
                    Capacity = cellPhone.Capacity,
                    Color = cellPhone.Color,
                    Name = cellPhone.Name,
                    Width = cellPhone.Width,
                    Height = cellPhone.Height,
                    Thickness = cellPhone.Thickness,
                    Weigth = cellPhone.Weigth,
                    ImageByte = cellPhone.Image,
                    Count = cellPhone.Count,
                    Price = cellPhone.Price,
                    CaseType = cellPhone.CaseType.GetDisplayName(),
                    Description = cellPhone.Description,
                }).ToList();
                if (!data.Any())
                {
                    return new BaseResponse<List<CellPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CellPhoneDTO>>()
                {
                    Description = $"[GetCellPhones] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> UpdateCellPhone(int id, CellPhoneDTO cellPhone)
        {
            try
            {
                var data = await _cellPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == id);
                if (data == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Телефон не найден",
                        StatusCode = StatusCode.InternalServerError,
                        Data = false
                    };
                }
                data.PhoneId = cellPhone.PhoneId;
                data.BrandId = cellPhone.BrandId;
                data.Camera = cellPhone.Camera;
                data.Capacity = cellPhone.Capacity;
                data.Color = cellPhone.Color;
                data.Name = cellPhone.Name;
                data.Width = cellPhone.Width;
                data.Height = cellPhone.Height;
                data.Thickness = cellPhone.Thickness;
                data.Weigth = cellPhone.Weigth;
                data.Image = cellPhone.ImageByte;
                data.Count = cellPhone.Count;
                data.Price = cellPhone.Price;
                data.CaseType = (CaseType)Convert.ToInt32(cellPhone.CaseType);
                data.Description = cellPhone.Description;
                var isDeleted = _cellPhoneRepository.Update(data);
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
                        Description = "Товар не удален",
                        Data = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateCellPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
    }
}
