using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowExtentionAttribute:ValidationAttribute
    {
        public AllowExtentionAttribute(string allowExtentions)
        {
            AllowExtentions = allowExtentions;
        }

        public string AllowExtentions { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extention = Path.GetExtension(file.FileName);

                var allowed = AllowExtentions.Split(',').Contains(extention, StringComparer.OrdinalIgnoreCase);
                if (!allowed)
                {
                    return new ValidationResult($"only {AllowExtentions} are allowed!");
                }
            }
            return ValidationResult.Success;
        }

    }
}
