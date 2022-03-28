using Travel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public class CommandService : ICommandService
    {
        private readonly ApplicationDbContext _context;

        public CommandService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Command> Create(Command command)
        {
            _context.Commands.Add(command);
            await _context.SaveChangesAsync();

            return command;
        }

        public async Task Delete(int id)
        {
            var commandToDelete = await _context.Commands.FindAsync(id);
            _context.Commands.Remove(commandToDelete);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Command>> GetAll()
        {
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command> GetById(int id)
        {
            return await _context.Commands.FindAsync(id);
        }

        public async Task Update(Command command)
        {
            _context.Entry(command).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}

