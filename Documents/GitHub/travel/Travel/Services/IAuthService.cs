using Travel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginUser model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task EditUser(ApplicationUser user);
        Task<ApplicationUser> GetUser(string id);
        Task DeleteUser(string id);
    }
}
