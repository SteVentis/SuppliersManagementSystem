using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Validations
{
    public class SupplierValidator : AbstractValidator<ValidatorBase>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(0, 10);
            RuleFor(x => x.TaxIdentNumber).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
        }
    }
}
