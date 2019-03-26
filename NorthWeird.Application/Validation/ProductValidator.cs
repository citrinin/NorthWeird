using FluentValidation;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(1).MaximumLength(40).NotEmpty();
            RuleFor(p => p.QuantityPerUnit).MaximumLength(30).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitsInStock).GreaterThan((short)0);
            RuleFor(p => p.UnitsOnOrder).GreaterThan((short)0);
            RuleFor(p => p.ReorderLevel).GreaterThan((short)0);
        }
    }
}
