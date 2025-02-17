using CourseProject.Data;
using CourseProject.Data.DTO.User;
using System.Security.Claims;

namespace CourseProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterDTO model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginDTO model);
    }
}
