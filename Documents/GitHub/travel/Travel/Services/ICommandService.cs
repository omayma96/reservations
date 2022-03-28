using Travel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public interface ICommandService
    {
        Task<Command> Create(Command command);
        Task Update(Command command);
        Task Delete(int id);
        Task<IEnumerable<Command>> GetAll();
        Task<Command> GetById(int id);
    }
}
