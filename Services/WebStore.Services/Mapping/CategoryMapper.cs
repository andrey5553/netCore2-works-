using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class CategoryMapper
    {
        public static SectionDTO ToDTO(this Category section) => section is null ? null : new SectionDTO
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
        };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Category> sections) => sections.Select(ToDTO);

        public static Category FromDTO(this SectionDTO section) => section is null ? null : new Category
        {
            Id = section.Id,
            Name = section.Name,
            ParentId = section.ParentId,
        };
    }
}
