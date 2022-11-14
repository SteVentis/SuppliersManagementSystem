using FluentValidation;
using Models.Dtos;

namespace SuppliersApi.Validations
{
    public class SupplierValidator : AbstractValidator<SupplierCreateOrUpdateDto>
    {
        public SupplierValidator()
        {

            RuleFor(x => x.Name).Length(3, 40);
            RuleFor(x => x.TaxIdentNumber).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address is Required")
                .EmailAddress().WithMessage("A valid email address is Required");
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
        }


    }
}
