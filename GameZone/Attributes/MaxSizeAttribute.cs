using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class MaxSizeAttribute:ValidationAttribute
    {
        public MaxSizeAttribute(int size)
        {
            Size = size;
        }

        public int Size { get; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                
                if(file.Length > Size)
                {
                    return new ValidationResult($"only size {Size} in Bytes");

                }
            }
            return ValidationResult.Success;
        }
    }
}
