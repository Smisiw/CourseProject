using CourseProject.Data.DTO.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using CourseProject.Services.Interfaces;

namespace CourseProject.Pages.Registration
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty]
        public RegisterDTO model { get; set; }

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
                var response = await _userService.Register(model);
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
