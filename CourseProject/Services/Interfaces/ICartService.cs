using CourseProject.Data;
using CourseProject.Data.DTO.CartItem;
using CourseProject.Data.Models;

namespace CourseProject.Services.Interfaces
{
    public interface ICartService
    {
        Task<BaseResponse<bool>> AddItemToCart(string login, int phoneId);
        Task<BaseResponse<bool>> RemoveItemFromCart(string login, int phoneId);
        Task<BaseResponse<bool>> ReduceItemCountCart(string login, int phoneId);
        Task<BaseResponse<List<CartItemDTO>>> GetCartItems(string login);
        Task<BaseResponse<Cart>> GetCart(string login);
    }
}
