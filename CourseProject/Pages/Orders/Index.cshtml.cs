using CourseProject.Data.DTO.Order;
using CourseProject.Services.Implementations;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseProject.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IPhoneService _phoneService;
        public IndexModel(IOrderService orderService, IPhoneService phoneService)
        {
            _orderService = orderService;
            _phoneService = phoneService;
        }

        public List<OrderDTO> Orders { get; set; }
        public void OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Redirect("/Login");
            }
            Orders = _orderService.GetOrders(User.Identity.Name).Result.Data;
            foreach (var order  in Orders)
            {
                order.OrderItems = _orderService.GetOrderItems(User.Identity.Name, order.OrderId).Result.Data;
                foreach (var orderItem in order.OrderItems)
                {
                    orderItem.Phone = _phoneService.GetPhone(orderItem.PhoneId).Result.Data;
                }
            }
        }
    }
}
