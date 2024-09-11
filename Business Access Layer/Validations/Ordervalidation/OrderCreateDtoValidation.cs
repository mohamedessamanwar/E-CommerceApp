using BusinessAccessLayer.Services.AddressService;
using DataAccessLayer.UnitOfWorkRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Validations.Ordervalidation
{
    public class OrderCreateDtoValidation : ValidationAttribute
    {

        private readonly IAddressService addressService;
        public OrderCreateDtoValidation(Func<string> errorMessageAccessor, IAddressService addressService) : base(errorMessageAccessor)
        {
            this.addressService = addressService;
        }

        protected override   ValidationResult IsValid
          (object? value, ValidationContext validationContext)
        {
            if (value is int AddressID)
            {
                var add =  addressService.GetAddressByUserId(AddressID);
                if (add == null)
                {
                    return new ValidationResult("Address Not Found");
                }
            }
            else
            {
                return new ValidationResult("Address value is not correct ");
            }
            return ValidationResult.Success;
        }
    }
}
