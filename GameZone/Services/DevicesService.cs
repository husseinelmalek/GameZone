using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{

    public class DevicesService : IDevicesService
    {
        public DevicesService(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public IEnumerable<SelectListItem> GetSelectedList()
        {
            return Context.Devices.Select(d => new SelectListItem()
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }
    }
}
