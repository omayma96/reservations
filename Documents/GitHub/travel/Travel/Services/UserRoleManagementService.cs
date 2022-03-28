using Microsoft.EntityFrameworkCore;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Models;

namespace Travel.Services
{
    public class UserRoleManagementService : IUserRoleManagementService
    {

        private readonly ApplicationDbContext _context;

        public UserRoleManagementService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<UserRole> Create(UserRole role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<UserRole> FetchById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task RemoveRole(string role)
        {
            var roleToDelete = await _context.Roles.FindAsync(role);
            _context.Roles.Remove(roleToDelete);
            await _context.SaveChangesAsync();

        }

    }
}
