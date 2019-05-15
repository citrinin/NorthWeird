using FluentValidation;
using NorthWeird.Application.Models;

namespace NorthWeird.Application.Validation
{
    public class CategoryValidator: AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).MinimumLength(2).MaximumLength(15).NotEmpty();
        }
    }
}
