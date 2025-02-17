using CourseProject.Data.DTO.CartItem;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseProject.Data.Models;

namespace CourseProject.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IPhoneService _phoneService;
        private readonly IOrderService _orderService;
        public IndexModel(ICartService cartService, IPhoneService phoneService, IOrderService orderService)
        {
            _cartService = cartService;
            _phoneService = phoneService;
            _orderService = orderService;
        }

        public List<CartItemDTO> CartItems {  get; set; }
        public decimal TotalPrice { get; set; }
        public void OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Redirect("/Login");
            }
            CartItems = _cartService.GetCartItems(User.Identity.Name).Result.Data.OrderBy(x => x.PhoneId).ToList();
            TotalPrice = CartItems.Sum(x => x.Price * x.Count);
            foreach (var cartItem in CartItems)
            {
                cartItem.Phone = _phoneService.GetPhone(cartItem.PhoneId).Result.Data;
            }
        }

        public async Task OnPostDeleteAsync(int phoneId)
        {
            await _cartService.RemoveItemFromCart(User.Identity.Name, phoneId);
            CartItems = _cartService.GetCartItems(User.Identity.Name).Result.Data.OrderBy(x => x.PhoneId).ToList();
            TotalPrice = CartItems.Sum(x => x.Price * x.Count);
            foreach (var cartItem in CartItems)
            {
                cartItem.Phone = _phoneService.GetPhone(cartItem.PhoneId).Result.Data;
            }
        }

        public async Task OnPostPlusAsync(int phoneId)
        {
            await _cartService.AddItemToCart(User.Identity.Name, phoneId);
            CartItems = _cartService.GetCartItems(User.Identity.Name).Result.Data.OrderBy(x => x.PhoneId).ToList();
            TotalPrice = CartItems.Sum(x => x.Price * x.Count);
            foreach (var cartItem in CartItems)
            {
                cartItem.Phone = _phoneService.GetPhone(cartItem.PhoneId).Result.Data;
            }
        }

        public async Task OnPostMinusAsync(int phoneId)
        {
            await _cartService.ReduceItemCountCart(User.Identity.Name, phoneId);
            CartItems = _cartService.GetCartItems(User.Identity.Name).Result.Data.OrderBy(x => x.PhoneId).ToList();
            TotalPrice = CartItems.Sum(x => x.Price * x.Count);
            foreach (var cartItem in CartItems)
            {
                cartItem.Phone = _phoneService.GetPhone(cartItem.PhoneId).Result.Data;
            }
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            await _orderService.CreateOrder(User.Identity.Name);
            return Redirect($"/Orders");
        }
    }
}
