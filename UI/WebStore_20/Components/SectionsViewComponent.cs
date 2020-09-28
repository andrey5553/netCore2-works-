using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    //[ViewComponent(Name = "Section")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductService _ProductData;

        public SectionsViewComponent(IProductService ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke() => View(GetSections());

        //public async Task<IViewComponentResult> Invoke() => View();

        private IEnumerable<CategoryViewModel> GetSections()
        {
            var sections = _ProductData.GetCategories().ToArray();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_views = parent_sections
               .Select(s => new CategoryViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                })
               .ToList();

            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in childs)
                    parent_section.ChildCategories.Add(new CategoryViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentCategory = parent_section
                    });

                parent_section.ChildCategories.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            return parent_sections_views;
        }
    }
}
