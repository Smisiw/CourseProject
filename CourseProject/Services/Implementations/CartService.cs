using CourseProject.Data;
using CourseProject.Data.DTO.CartItem;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Cart> _cartRepository;
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<Phone> _phoneRepository;
        public CartService(IBaseRepository<User> userRepository,
            IBaseRepository<Cart> cartRepository,
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<Phone> phoneRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _phoneRepository = phoneRepository;
        }

        public async Task<BaseResponse<bool>> AddItemToCart(string login, int phoneId)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var phone = await _phoneRepository.GetAll().FirstOrDefaultAsync(x => x.PhoneId == phoneId);
                if (phone == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Телефон не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cartItem = _cartItemRepository.GetAll().FirstOrDefault(x => x.PhoneId == phoneId);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        PhoneId = phoneId,
                        Count = 1,
                        Price = phone.Price
                    };
                    await _cartItemRepository.CreateAsync(cartItem);
                    return new BaseResponse<bool>
                    {
                        StatusCode = Data.Enum.StatusCode.OK,
                        Data = true,
                    };
                }
                if (cartItem.Count < phone.Count)
                {
                    cartItem.Count++;
                    cartItem.Price = phone.Price;
                    _cartItemRepository.Update(cartItem);
                    return new BaseResponse<bool>
                    {
                        StatusCode = Data.Enum.StatusCode.OK,
                        Data = true,
                    };
                }
                return new BaseResponse<bool>
                {
                    Description = "Максимальное число данного товара в корзине",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<Cart>> GetCart(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<Cart>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<Cart>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                return new BaseResponse<Cart>
                {
                    StatusCode = Data.Enum.StatusCode.OK,
                    Data = cart
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Cart>
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }

        }

        public async Task<BaseResponse<List<CartItemDTO>>> GetCartItems(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<List<CartItemDTO>>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<List<CartItemDTO>>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                var data = await _cartItemRepository.GetAll().Select(x => new CartItemDTO
                {
                    CartId = x.CartId,
                    CartItemId = x.CartItemId,
                    PhoneId = x.PhoneId,
                    Count = x.Count,
                    Price = x.Price,
                }).ToListAsync();
                return new BaseResponse<List<CartItemDTO>>
                {
                    StatusCode = Data.Enum.StatusCode.OK,
                    Data = data
                };
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CartItemDTO>>
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<bool>> ReduceItemCountCart(string login, int phoneId)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cartItem = _cartItemRepository.GetAll().FirstOrDefault(x => x.PhoneId == phoneId);
                if (cartItem == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Товар не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false,
                    };
                }
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    _cartItemRepository.Update(cartItem);
                    return new BaseResponse<bool>
                    {
                        StatusCode = Data.Enum.StatusCode.OK,
                        Data = true,
                    };
                }
                else
                {
                    await _cartItemRepository.DeleteAsync(cartItem.CartItemId);
                    return new BaseResponse<bool>
                    {
                        StatusCode = Data.Enum.StatusCode.OK,
                        Data = true,
                    };
                }
                return new BaseResponse<bool>
                {
                    Description = "Непредвиденная ошибка",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<bool>> RemoveItemFromCart(string login, int phoneId)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false
                    };
                }
                var cartItem = _cartItemRepository.GetAll().FirstOrDefault(x => x.PhoneId == phoneId);
                if (cartItem == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Товар не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                        Data = false,
                    };
                }
                await _cartItemRepository.DeleteAsync(cartItem.CartItemId);
                return new BaseResponse<bool>
                {
                    StatusCode = Data.Enum.StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = ex.Message,
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }
    }
}
