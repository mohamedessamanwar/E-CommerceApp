using BusinessAccessLayer.DTOS.ProductDtos;
using FluentValidation;
using FluentValidation.Results;

namespace Business_Access_Layer.Validations.ProductValidation
{
    public class CreateProductDtoValidation : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidation()
        {
            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("Name must not be empty")
           .MaximumLength(255).WithMessage("Name must be less than or equal to 255 characters");
            RuleFor(p => p.Model).MaximumLength(255).WithMessage("Max length 255 Word").NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty")
               .NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty");
            RuleFor(p => p.Description).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty");
            RuleFor(p => p.Price).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty").GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(p => p.CategoryID).NotNull().WithMessage("Must Not Empty").NotEmpty().WithMessage("Must Not Empty");
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

        public Task<ValidationResult> ValidateAsync(CreateProductDto createProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
