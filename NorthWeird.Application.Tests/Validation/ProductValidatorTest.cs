using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NorthWeird.Application.Tests.Infrastructure;
using NorthWeird.Application.Validation;
using NUnit.Framework;

namespace NorthWeird.Application.Tests.Validation
{
    public class ProductValidatorTest
    {
        private ProductValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ProductValidator();
        }

        [Test]
        public void ProductNameIsNull_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.ProductName, null as string);
        }

        [Test]
        public void ProductNameIsEmptyString_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.ProductName, "");
        }

        [Test]
        public void ProductNameIsLongerThanFortyChars_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.ProductName, new string('a', 41));
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(40)]
        public void ProductNameIsBetweenOneAndFortyChars_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.ProductName, new string('a', stringLength));
        }

        [Test]
        public void QuantityPerUnitIsNull_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.QuantityPerUnit, null as string);
        }

        [Test]
        public void QuantityPerUnitIsEmptyString_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.QuantityPerUnit, "");
        }

        [Test]
        public void QuantityPerUnitIsLongerThanThirtyChars_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.QuantityPerUnit, new string('a', 31));
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(30)]
        public void QuantityPerUnitIsBetweenZeroAndThirtyChars_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.QuantityPerUnit, new string('a', stringLength));
        }

        [Test]
        public void UnitPriceBelowZero_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.UnitPrice, -5);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(int.MaxValue)]
        public void UnitPriceAboveZero_ShouldNotHaveError(decimal unitPrice)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.UnitPrice, unitPrice);
        }

        [Test]
        public void UnitsInStockBelowZero_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.UnitsInStock, (short)-5);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(short.MaxValue)]
        public void UnitsInStockAboveZero_ShouldNotHaveError(short unitsInStock)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.UnitsInStock, unitsInStock);
        }

        [Test]
        public void UnitsOnOrderBelowZero_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.UnitsOnOrder, (short)-5);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(short.MaxValue)]
        public void UnitsOnOrderAboveZero_ShouldNotHaveError(short unitsOnOrder)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.UnitsOnOrder, unitsOnOrder);
        }

        [Test]
        public void ReorderLevelBelowZero_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(product => product.ReorderLevel, (short)-5);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(short.MaxValue)]
        public void ReorderLevelAboveZero_ShouldNotHaveError(short reorderLevel)
        {
            _validator.ShouldNotHaveValidationErrorFor(product => product.ReorderLevel, reorderLevel);
        }
    }
}
