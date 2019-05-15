using FluentValidation;
using NorthWeird.Application.Models;

namespace NorthWeird.Application.Validation
{
    public class SupplierValidator: AbstractValidator<SupplierDto>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.CompanyName).MaximumLength(40).NotEmpty();
            RuleFor(s => s.ContactName).MinimumLength(2).MaximumLength(30);
            RuleFor(s => s.ContactTitle).MinimumLength(2).MaximumLength(30);
            RuleFor(s => s.Address).MinimumLength(5).MaximumLength(60);
            RuleFor(s => s.City).MinimumLength(2).MaximumLength(15);
            RuleFor(s => s.Region).MinimumLength(2).MaximumLength(15);
            RuleFor(s => s.Country).MinimumLength(2).MaximumLength(15);
            RuleFor(s => s.PostalCode).MinimumLength(2).MaximumLength(10);
            RuleFor(s => s.Phone).MinimumLength(2).MaximumLength(24);
            RuleFor(s => s.Fax).MinimumLength(2).MaximumLength(24);
            RuleFor(s => s.HomePage).MinimumLength(4);
        }
    }
}
