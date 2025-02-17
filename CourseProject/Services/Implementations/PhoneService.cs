using CourseProject.Data;
using CourseProject.Data.DTO.CellPhone;
using CourseProject.Data.DTO.Phone;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Enum;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Data.Repositories;
using CourseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services.Implementations
{
    public class PhoneService : IPhoneService
    {
        private readonly IBaseRepository<Phone> _phoneRepository;
        public PhoneService(IBaseRepository<Phone> phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<BaseResponse<PhoneDTO>> GetPhone(int id)
        {
            try
            {
                var phone = await _phoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == id);
                if (phone == null)
                {
                    return new BaseResponse<PhoneDTO>()
                    {
                        Description = "Телефон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                if (phone is SmartPhone)
                {
                    SmartPhone smartPhone = (SmartPhone)phone;
                    var data = new SmartPhoneDTO
                    {
                        PhoneId = smartPhone.PhoneId,
                        BrandId = smartPhone.BrandId,
                        Camera = smartPhone.Camera,
                        Capacity = smartPhone.Capacity,
                        Color = smartPhone.Color,
                        Memory = smartPhone.Memory,
                        Name = smartPhone.Name,
                        OS = smartPhone.OS,
                        OSVersion = smartPhone.OSVersion,
                        Ram = smartPhone.Ram,
                        Width = smartPhone.Width,
                        Height = smartPhone.Height,
                        Thickness = smartPhone.Thickness,
                        Weigth = smartPhone.Weigth,
                        ImageByte = smartPhone.Image,
                        CoreNumber = smartPhone.CoreNumber,
                        Frequency = smartPhone.Frequency,
                        Count = smartPhone.Count,
                        Price = smartPhone.Price,
                        Description = smartPhone.Description,
                    };
                    return new BaseResponse<PhoneDTO>()
                    {
                        StatusCode = StatusCode.OK,
                        Data = data
                    };
                }
                else
                {
                    CellPhone cellPhone = (CellPhone)phone;
                    var data = new CellPhoneDTO()
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
                    return new BaseResponse<PhoneDTO>()
                    {
                        StatusCode = StatusCode.OK,
                        Data = data
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<PhoneDTO>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<PhoneDTO>>> GetPhone(string term)
        {
            try
            {
                var data = await _phoneRepository.GetAll().Where(x => EF.Functions.Like(x.Name, $"%{term}%")).ToListAsync();
                List<PhoneDTO> result = new List<PhoneDTO>();
                foreach (var phone in data)
                {
                    if (phone is SmartPhone)
                    {
                        SmartPhone smartPhone = (SmartPhone)phone;
                        result.Add(new SmartPhoneDTO
                        {
                            PhoneId = smartPhone.PhoneId,
                            BrandId = smartPhone.BrandId,
                            Camera = smartPhone.Camera,
                            Capacity = smartPhone.Capacity,
                            Color = smartPhone.Color,
                            Memory = smartPhone.Memory,
                            Name = smartPhone.Name,
                            OS = smartPhone.OS,
                            OSVersion = smartPhone.OSVersion,
                            Ram = smartPhone.Ram,
                            Width = smartPhone.Width,
                            Height = smartPhone.Height,
                            Thickness = smartPhone.Thickness,
                            Weigth = smartPhone.Weigth,
                            ImageByte = smartPhone.Image,
                            CoreNumber = smartPhone.CoreNumber,
                            Frequency = smartPhone.Frequency,
                            Count = smartPhone.Count,
                            Price = smartPhone.Price,
                            Description = smartPhone.Description
                        });
                    }
                    else
                    {
                        CellPhone cellPhone = (CellPhone)phone;
                        result.Add(new CellPhoneDTO
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
                        });
                    }
                    
                }
                if (!result.Any())
                {
                    return new BaseResponse<List<PhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<PhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<PhoneDTO>>> GetPhoneByBrand(int brandId)
        {
            try
            {
                var data = await _phoneRepository.GetAll().Where(x => x.BrandId == brandId).ToListAsync();
                List<PhoneDTO> result = new List<PhoneDTO>();
                foreach (var phone in data)
                {
                    if (phone is SmartPhone)
                    {
                        SmartPhone smartPhone = (SmartPhone)phone;
                        result.Add(new SmartPhoneDTO
                        {
                            PhoneId = smartPhone.PhoneId,
                            BrandId = smartPhone.BrandId,
                            Camera = smartPhone.Camera,
                            Capacity = smartPhone.Capacity,
                            Color = smartPhone.Color,
                            Memory = smartPhone.Memory,
                            Name = smartPhone.Name,
                            OS = smartPhone.OS,
                            OSVersion = smartPhone.OSVersion,
                            Ram = smartPhone.Ram,
                            Width = smartPhone.Width,
                            Height = smartPhone.Height,
                            Thickness = smartPhone.Thickness,
                            Weigth = smartPhone.Weigth,
                            ImageByte = smartPhone.Image,
                            CoreNumber = smartPhone.CoreNumber,
                            Frequency = smartPhone.Frequency,
                            Count = smartPhone.Count,
                            Price = smartPhone.Price,
                            Description = smartPhone.Description
                        });
                    }
                    else
                    {
                        CellPhone cellPhone = (CellPhone)phone;
                        result.Add(new CellPhoneDTO
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
                        });
                    }

                }
                if (!result.Any())
                {
                    return new BaseResponse<List<PhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<PhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<List<PhoneDTO>> GetPhones()
        {
            try
            {
                var data = _phoneRepository.GetAll().ToList();
                List<PhoneDTO> result = new List<PhoneDTO>();
                foreach (var phone in data)
                {
                    if (phone is SmartPhone)
                    {
                        SmartPhone smartPhone = (SmartPhone)phone;
                        result.Add(new SmartPhoneDTO
                        {
                            PhoneId = smartPhone.PhoneId,
                            BrandId = smartPhone.BrandId,
                            Camera = smartPhone.Camera,
                            Capacity = smartPhone.Capacity,
                            Color = smartPhone.Color,
                            Memory = smartPhone.Memory,
                            Name = smartPhone.Name,
                            OS = smartPhone.OS,
                            OSVersion = smartPhone.OSVersion,
                            Ram = smartPhone.Ram,
                            Width = smartPhone.Width,
                            Height = smartPhone.Height,
                            Thickness = smartPhone.Thickness,
                            Weigth = smartPhone.Weigth,
                            ImageByte = smartPhone.Image,
                            CoreNumber = smartPhone.CoreNumber,
                            Frequency = smartPhone.Frequency,
                            Count = smartPhone.Count,
                            Price = smartPhone.Price,
                            Description = smartPhone.Description
                        });
                    }
                    else
                    {
                        CellPhone cellPhone = (CellPhone)phone;
                        result.Add(new CellPhoneDTO
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
                        });
                    }

                }
                if (!result.Any())
                {
                    return new BaseResponse<List<PhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<PhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
