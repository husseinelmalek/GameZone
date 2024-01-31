using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        public CategoriesService(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public IEnumerable<SelectListItem> GetSelectedList()
        {
            return Context.Categories.Select(c => new SelectListItem()
            {
                Value= c.Id.ToString(),
                Text=c.Name
            }).OrderBy(c=>c.Text).ToList();
        }
    }
}
