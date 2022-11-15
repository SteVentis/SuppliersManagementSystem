using FluentValidation;
using Models.Dtos;
using System.Collections.Generic;

namespace SuppliersApi.Validations
{
    public class SupplierValidator : AbstractValidator<SupplierCreateOrUpdateDto>
    {
        public SupplierValidator()
        {

            RuleFor(x => x.Name).Length(3, 40);
            RuleFor(x => x.TaxIdentNumber).NotNull().NotEmpty()
                .Must(x => IsTaxIdentNumbValid(x) == true).WithMessage("Your Tax Identiy Number is not valid");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address is Required")
                .EmailAddress().WithMessage("A valid email address is Required");
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
        }

        public static bool IsTaxIdentNumbValid(int taxIdentNum)
        {
            List<int> afmDigits = new List<int>();
            while (taxIdentNum > 0)
            {
                var digit = taxIdentNum % 10;
                taxIdentNum /= 10;
                afmDigits.Add(digit);
            }
            if (afmDigits.Count != 9)
            {
                return false;
            }
            else
            {
                int sum = 0;
                int multiplier = 2;
                int remainder = 0;
                for (int i = 1; i <= 8; i++)
                {
                    sum += multiplier * afmDigits[i];
                    multiplier += multiplier;
                }
                remainder = (sum % 11) % 10;
                return afmDigits[0] == remainder;
            }
        }

    }
}
