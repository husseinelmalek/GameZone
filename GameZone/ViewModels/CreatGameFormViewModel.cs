using GameZone.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModels
{
    public class CreatGameFormViewModel:GameFormViewModel
    {
      

        [AllowExtention(FileSettings.AllowExtentions)]
        [MaxSize(FileSettings.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;
    }
}
