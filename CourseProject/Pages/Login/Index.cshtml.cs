using CourseProject.Data.DTO.User;
using CourseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CourseProject.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty]
        public LoginDTO model { get; set; }

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);
                if (response.StatusCode == Data.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            return Page();
        }
    }
}
