using CourseProject.Data;
using CourseProject.Data.DTO.Order;
using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using CourseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderItem> _orderItemRepository;
        private readonly IBaseRepository<Cart> _cartRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Phone> _phoneRepository;
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderService(IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderItem> orderItemRepository,
            IBaseRepository<Cart> cartRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<Phone> phoneRepository,
            IBaseRepository<CartItem> cartItemRepository,
            ApplicationDbContext applicationDbContext)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
            _cartItemRepository = cartItemRepository;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BaseResponse<OrderDTO>> CreateOrder(string login)
        {
            try
            {
                using var transaction = _applicationDbContext.Database.BeginTransaction();
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return new BaseResponse<OrderDTO>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
                if (cart == null)
                {
                    return new BaseResponse<OrderDTO>
                    {
                        Description = "Корзина не найдена",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                var cartItems = _cartItemRepository.GetAll().Where(x => x.CartId == cart.CartId).ToList();
                if (!cartItems.Any())
                {
                    return new BaseResponse<OrderDTO>
                    {
                        Description = "Товары в корзине не найдены",
                        StatusCode = Data.Enum.StatusCode.InternalServerError,
                    };
                }
                Order order = new Order
                {
                    UserId = user.UserId,
                    CreatedDate = DateTime.Now
                };
                await _orderRepository.CreateAsync(order);
                foreach (var cartItem in cartItems)
                {
                    var phone = _phoneRepository.GetAll().FirstOrDefault(x => x.PhoneId == cartItem.PhoneId);
                    if (phone == null || phone.Count < cartItem.Count)
                    {
                        await transaction.DisposeAsync();
                    }
                    else
                    {
                        phone.Count -= cartItem.Count;
                        _phoneRepository.Update(phone);
                        OrderItem orderItem = new OrderItem
                        {
                            OrderId = order.OrderId,
                            PhoneId = cartItem.PhoneId,
                            Count = cartItem.Count,
                            Price = cartItem.Price,
                        };
                        await _orderItemRepository.CreateAsync(orderItem);
                        await _cartItemRepository.DeleteAsync(cartItem.CartItemId);
                    }
                }
                var data = new OrderDTO
                {
                    OrderId = order.OrderId,
                    CreatedDate = order.CreatedDate,
                    UserId = user.UserId,
                };
                transaction.Commit();
                return new BaseResponse<OrderDTO>
                {
                    StatusCode = Data.Enum.StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDTO>
                {
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                    Description = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<List<OrderItemDTO>>> GetOrderItems(string login, int orderId)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
            if (user == null)
            {
                return new BaseResponse<List<OrderItemDTO>>
                {
                    Description = "Пользователь не найден",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
            var orderItems = await _orderItemRepository.GetAll().Where(x => x.OrderId == orderId).Select(x => new OrderItemDTO
            {
                OrderId = x.OrderId,
                OrderItemId = x.OrderItemId,
                Count = x.Count,
                PhoneId = x.PhoneId,
                Price = x.Price,
            }).ToListAsync();
            return new BaseResponse<List<OrderItemDTO>>
            {
                Data = orderItems,
                StatusCode = Data.Enum.StatusCode.OK,
            };
        }

        public async Task<BaseResponse<List<OrderDTO>>> GetOrders(string login)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);
            if (user == null)
            {
                return new BaseResponse<List<OrderDTO>>
                {
                    Description = "Пользователь не найден",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
            var orders = await _orderRepository.GetAll().Select(x => new OrderDTO
            {
                OrderId=x.OrderId,
                CreatedDate = x.CreatedDate,
                UserId = x.UserId,
            }).ToListAsync();
            return new BaseResponse<List<OrderDTO>>
            {
                Data = orders,
                StatusCode = Data.Enum.StatusCode.OK,
            };
        }
    }
}
