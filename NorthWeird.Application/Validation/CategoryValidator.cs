using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using NorthWeird.Domain.Entities;

namespace NorthWeird.Application.Validation
{
    public class CategoryValidator: AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).MinimumLength(2).MaximumLength(15).NotEmpty();
        }
    }
}
