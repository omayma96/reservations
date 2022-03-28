using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Models;

namespace Travel.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public CategoryService(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public int Count()
        {
            return _context.Categories.Count();
        }

        public async Task<Tuple<int, List<Category>>> FetchPage(int page, int pageSize)
        {
            var queryable = _context.Categories;
            var count = await queryable.CountAsync();
            var results = await queryable.Include(t => t.CategoryImages).Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return await Task.FromResult(Tuple.Create(count, results));
        }

        public async Task<Tuple<int, List<Category>>> FetchPageWithImages(int page, int pageSize)
        {
            var queryable = _context.Categories.Include(c => c.CategoryImages)
                .Where(t => t.CategoryImages != null && t.CategoryImages.Count > 0);
            var count = await queryable.CountAsync();
            var results = await queryable.Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return await Task.FromResult(Tuple.Create(count, results));
        }

        public Category FetchById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CatID == id);
        }


        public async Task<Category> Create(string name, string description, List<IFormFile> files,
            long? userId = null)
        {
            ICollection<CategoryImage> fileUploads = new List<CategoryImage>(files.Count);
            foreach (IFormFile file in files)
            {
                FileUpload fileUpload = await _storageService.UploadFormFile(file, "categories");

                fileUploads.Add(new CategoryImage
                {
                    OriginalFileName = fileUpload.OriginalFileName,
                    FileName = fileUpload.FileName,
                    FilePath = fileUpload.FilePath,
                    FileSize = file.Length
                });
            }


            var category = new Category
            {
                Nom = name,
                Description = description,
                CategoryImages = fileUploads
            };
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
            return category;
        }


        public void Create(Category cat)
        {
            _context.Categories.Add(cat);
        }


        public async Task Update(Category cat)
        {
            _context.Categories.Update(cat);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var catToDelete = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(catToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
