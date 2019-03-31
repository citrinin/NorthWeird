using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NorthWeird.Application.Validation;
using NUnit.Framework;

namespace NorthWeird.Application.Tests.Validation
{
    public class CategoryValidatorTest
    {
        private readonly CategoryValidator _validator;

        public CategoryValidatorTest()
        {
            _validator = new CategoryValidator();
        }

        [Test]
        public void CategoryNameIsNull_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(category => category.CategoryName, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void CategoryNameIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(category => category.CategoryName, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(15)]
        public void CategoryNameIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(category => category.CategoryName, new string('a', stringLength));
        }

        [TestCase(16)]
        public void CategoryNameIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(category => category.CategoryName, new string('a', stringLength));
        }
    }
}
