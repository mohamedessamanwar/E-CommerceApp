using BusinessAccessLayer.DTOS.ProductDtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace BusinessAccessLayer.Validations.ProductValidation
{
    public class CreateProductDtoValidation : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidation()
        {
            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("Name must not be empty")
           .MaximumLength(255).WithMessage("Name must be less than or equal to 255 characters");
            RuleFor(p => p.Model).MaximumLength(255).WithMessage("Max length 255 Word").NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty")
  ;
            RuleFor(p => p.Description).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty");
            RuleFor(p => p.Price).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty").GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(p => p.CategoryID).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty");
            // Conditionally apply the custom validation rule only if Cover is not null and not empty
            RuleFor(p => p.Cover)
                .Must(VlidateCoverType).WithMessage("Type is not correct.")
                .When(p => p.Cover != null );
            // RuleFor(p=> p.Cover).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty").Must(VlidateCoverTupe).WithMessage("type is not correct .");
            RuleFor(p => p.Cover)
                .Must(VlidateCoverSize).WithMessage("Size is not correct.")
                .When(p => p.Cover != null);
        }

        private bool VlidateCoverSize(List<IFormFile> list)
        {
            var size = Helper.Helper.MaxFileSizeInBytes;
            foreach (var cover in list)
            {

                var Result = cover.Length < size ;
                if (!Result)
                {
                    return false;
                }
            }
            return true;

        }

        private bool VlidateCoverType(List<IFormFile> list)
        {
            string[] Types = Helper.Helper.AllowedExtensions.Split(',');
            foreach (var cover in list) {
                var extension = Path.GetExtension(cover.FileName);
                var Result = Types.Contains(extension);
                if (!Result) { 
                    return false;  
                }
            }
            return true;
        }

        public Dictionary<string, List<string>> ListError(ValidationResult validationResult)
        {
            Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (!errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName] = new List<string>();
                }
                errors[error.PropertyName].Add(error.ErrorMessage);
            }
            return errors;

        }

        
    }
}
