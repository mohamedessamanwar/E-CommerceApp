using BusinessAccessLayer.DTOS.ReviewDtos;
using BusinessAccessLayer.Exception;
using FluentValidation;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Validations.ReviewValidation
{
    public class CreateReview : AbstractValidator<AddReview>
    {
        public CreateReview()
        {
            RuleFor(x => x.Comment).MaximumLength(500).WithMessage("limit 500 character");            
                

            RuleFor(x => x.ProductId).NotNull()
                .NotEmpty().WithMessage("Product is required.");
               
            RuleFor(x => x.Rate).NotEmpty().WithMessage("Rate is required.")
                .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");
        }
        public override FluentValidation.Results.ValidationResult Validate(ValidationContext<AddReview> context)
        {
            // Perform base validation
            var result = base.Validate(context);

            // If validation failed
            if (!result.IsValid)
            {
                // Group errors by property name and create a dictionary
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );
                // Throw a custom validation exception with the errors and a message
                throw new CustomValidationException(errors, "Error in Data");
            }

            // Return the validation result if valid
            return result;
        }
    }
}
