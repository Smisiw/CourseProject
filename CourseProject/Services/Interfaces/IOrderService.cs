using CourseProject.Data;
using CourseProject.Data.DTO.Order;
using CourseProject.Data.Models;

namespace CourseProject.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderDTO>> CreateOrder(string login);
        Task<BaseResponse<List<OrderDTO>>> GetOrders(string login);
        Task<BaseResponse<List<OrderItemDTO>>> GetOrderItems(string login, int orderId);
    }
}
