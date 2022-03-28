using Travel.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Dto
{
    public class CategoryDto : SuccessResponse
    {
        public long Id { get; set; }
        public List<string> ImageUrls { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public static CategoryDto Build(Models.Category category)
        {
            List<string> imageUrls = new List<string>();
          
            return new CategoryDto
            {
                Id = category.CatID,
                Name = category.Nom,
                Description = category.Description,
                ImageUrls = imageUrls
            };
        }
    }
}
