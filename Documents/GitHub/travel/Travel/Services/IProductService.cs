using Travel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public interface IProductService
    {
        Task<Product> Create(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
    }
}
