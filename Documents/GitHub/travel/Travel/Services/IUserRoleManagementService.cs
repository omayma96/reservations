using Microsoft.AspNetCore.Identity;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public interface IUserRoleManagementService
    {
        Task<UserRole> Create(UserRole role);
        Task<IEnumerable<UserRole>> GetAll();
        Task RemoveRole(string role);

        Task<UserRole> FetchById(int id);


    }
}
