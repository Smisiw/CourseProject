using CourseProject.Data;
using CourseProject.Data.DTO.SmartPhone;
using CourseProject.Data.Enum;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Data.Repositories;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
using System;

namespace CourseProject.Services.Implementations
{
    public class SmartPhoneService : ISmartPhoneService
    {
        private readonly IBaseRepository<SmartPhone> _smartPhoneRepository;
        public SmartPhoneService(IBaseRepository<SmartPhone> smartPhoneRepository)
        {
            _smartPhoneRepository = smartPhoneRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> CreateSmartPhone(SmartPhoneDTO smartPhone, byte[] imageData)
        {
            try
            {
                var data = new SmartPhone()
                {
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
                    Image = imageData,
                    CoreNumber = smartPhone.CoreNumber,
                    Frequency = smartPhone.Frequency,
                    Count = smartPhone.Count,
                    Price = smartPhone.Price,
                    Description = smartPhone.Description,
                };
                await _smartPhoneRepository.CreateAsync(data);
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
                    Description = $"[CreateSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> DeleteSmartPhone(int id)
        {
            try
            {
                var isDeleted = await _smartPhoneRepository.DeleteAsync(id);
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
                    Description = $"[DeleteSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<SmartPhoneDTO>> GetSmartPhone(int id)
        {
            try
            {
                var smartPhone = await _smartPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == id);
                if (smartPhone == null)
                {
                    return new BaseResponse<SmartPhoneDTO>()
                    {
                        Description = "Телефон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
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
                return new BaseResponse<SmartPhoneDTO>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SmartPhoneDTO>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhone(string term)
        {
            try
            {
                var data = await _smartPhoneRepository.GetAll().Select(smartPhone => new SmartPhoneDTO
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
                }).Where(x => EF.Functions.Like(x.Name, $"%{term}%")).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<SmartPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneByBrand(int brandId)
        {
            try
            {
                var data = await _smartPhoneRepository.GetAll().Select(smartPhone => new SmartPhoneDTO
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
                }).Where(x => x.BrandId == brandId).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<SmartPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    Description = $"[GetSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneColors(int phoneId)
        {
            try
            {
                var phone = await _smartPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == phoneId);
                if (phone == null)
                {
                    return new BaseResponse<List<SmartPhoneDTO>>()
                    {
                        Description = "Смартфон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                var sortedPhones = _smartPhoneRepository.GetAll().Select(smartPhone => new SmartPhoneDTO
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
                }).Where(x => x.Name == phone.Name).OrderByDescending(x => x.Memory == phone.Memory).ThenBy(x => x.Color).ToList();
                var data = new List<SmartPhoneDTO>();
                foreach (var smartPhone in sortedPhones)
                {
                    if (!data.Any(x => x.Color == smartPhone.Color))
                    {
                        data.Add(smartPhone);
                    }
                }
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    Description = $"[GetSmartPhoneColors] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<SmartPhoneDTO>>> GetSmartPhoneMemories(int phoneId)
        {
            try
            {
                var phone = await _smartPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == phoneId);
                if (phone == null)
                {
                    return new BaseResponse<List<SmartPhoneDTO>>()
                    {
                        Description = "Смартфон не найден",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                var data = _smartPhoneRepository.GetAll().Select(smartPhone => new SmartPhoneDTO
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
                }).Where(x => x.Name == phone.Name).Where(x => x.Color == phone.Color).OrderBy(x => x.Memory).ToList();
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    Description = $"[GetSmartPhoneMemories] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<List<SmartPhoneDTO>> GetSmartPhones()
        {
            try
            {
                var data = _smartPhoneRepository.GetAll().Select(smartPhone => new SmartPhoneDTO
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
                }).ToList();
                if (!data.Any())
                {
                    return new BaseResponse<List<SmartPhoneDTO>>()
                    {
                        Description = "Найдено 0 товаров",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<List<SmartPhoneDTO>>()
                {
                    Description = $"[GetSmartPhones] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<BaseResponse<bool>> UpdateSmartPhone(int id, SmartPhoneDTO smartPhone)
        {
            try
            {
                var data = await _smartPhoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == id);
                if (data == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Смартфон не найден",
                        StatusCode = StatusCode.InternalServerError,
                        Data = false
                    };
                }
                data.PhoneId = smartPhone.PhoneId;
                data.BrandId = smartPhone.BrandId;
                data.Camera = smartPhone.Camera;
                data.Capacity = smartPhone.Capacity;
                data.Color = smartPhone.Color;
                data.Memory = smartPhone.Memory;
                data.Name = smartPhone.Name;
                data.OS = smartPhone.OS;
                data.OSVersion = smartPhone.OSVersion;
                data.Ram = smartPhone.Ram;
                data.Width = smartPhone.Width;
                data.Height = smartPhone.Height;
                data.Thickness = smartPhone.Thickness;
                data.Weigth = smartPhone.Weigth;
                data.Image = smartPhone.ImageByte;
                data.CoreNumber = smartPhone.CoreNumber;
                data.Frequency = smartPhone.Frequency;
                data.Count = smartPhone.Count;
                data.Price = smartPhone.Price;
                data.Description = smartPhone.Description;
                var isDeleted = _smartPhoneRepository.Update(data);
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
                    Description = $"[UpdateSmartPhone] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
    }
}
