using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Models;

namespace Travel.Services
{
    public interface ICategoryService
    {
        int Count();

        Category FetchById(int id);
        void Create(Category cat);
        Task Update(Category cat);
        Task Delete(int id);

        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetById(int id);

        Task<Tuple<int, List<Category>>> FetchPageWithImages(int page, int pageSize);

        Task<Category> Create(string name, string description, List<IFormFile> files,
            long? userId = null);

        Task<Tuple<int, List<Category>>> FetchPage(int page, int pageSize);
    }
}
