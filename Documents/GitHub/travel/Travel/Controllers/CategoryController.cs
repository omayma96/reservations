
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Dto;
using Travel.Dto.Responses;
using Travel.Dto.Responses.category;
using Travel.Models;
using Travel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Travel.Controllers
{
    [Route("/api")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoriesService;

        public CategoryController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IEnumerable> GetCategories()
        {
            return await _categoriesService.GetCategories();

        }

        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> CreateCategory(string name, string description, List<IFormFile> images)
        {

            
            if (images?.Count == 0)
                images = Request.Form.Files.GetFiles("images[]").ToList();

            Category category = await _categoriesService.Create(name, description, images);
            return StatusCodeAndDtoWrapper.BuildSuccess(CategoryDto.Build(category), "Category created successfully");
        }

        [HttpGet]
        [Route("Category by Id")]
        public async Task<ActionResult<Category>> GetCat(int id)
        {
            return await _categoriesService.GetById(id);
        }

        [HttpPut("Update Category")]
        public async Task<ActionResult> PutCat(int id, [FromBody] Category cat)
        {
            if (id != cat.CatID)
            {
                return BadRequest();
            }

            await _categoriesService.Update(cat);

            return NoContent();
        }

        [HttpDelete("Delete Category")]
        public async Task<ActionResult> Delete(int id)
        {
            var catToDelete = await _categoriesService.GetById(id);
            if (catToDelete == null)
                return NotFound();

            await _categoriesService.Delete(catToDelete.CatID);
            return NoContent();
        }
    }
}
