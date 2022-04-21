using Amado.Core.DTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amado.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(AppUserDto entity, string password);
        Task<string> LoginAsync(string userName, string password, bool isPersistent);
        Task LogoutAsync();
        Task<string> NewEmailAsync(string email, string password, string newEmail);
        Task<string> NewPasswordAsync(string email, string password, string newPassword);
        Task<string> UpdateAsync(AppUserDto entity);
        Task<AppUserDto> GetbyIdAsync(string id);
        Task<AppUserDto> GetbyEmailAsync(string email);
        Task<bool> AnybyEmailAsync(string email);
        Task<bool> AnybyIdAsync(string id);
        List<AppUserDto> GetAllActive();
        bool IsSignIn(ClaimsPrincipal principal);
        Task<bool> CheckPasswordAsync(string userName, string password);
    }
}
