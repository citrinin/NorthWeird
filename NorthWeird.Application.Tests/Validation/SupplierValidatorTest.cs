using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NorthWeird.Application.Validation;
using NUnit.Framework;

namespace NorthWeird.Application.Tests.Validation
{
    public class SupplierValidatorTest
    {
        private readonly SupplierValidator _validator;

        public SupplierValidatorTest()
        {
            _validator = new SupplierValidator();
        }

        #region CompanyName

        [Test]
        public void CompanyNameIsNull_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.CompanyName, null as string);
        }

        [Test]
        public void CompanyNameIsEmptyString_ShouldHaveError()
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.CompanyName, "");
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(40)]
        public void CompanyNameIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.CompanyName, new string('a', stringLength));
        }

        [TestCase(41)]
        public void CompanyNameIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.CompanyName, new string('a', stringLength));
        }

        #endregion

        #region ContactName

        [Test]
        public void ContactNameIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.ContactName, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void ContactNameIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.ContactName, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(20)]
        [TestCase(30)]
        public void ContactNameIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.ContactName, new string('a', stringLength));
        }

        [TestCase(31)]
        public void ContactNameIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.ContactName, new string('a', stringLength));
        }

        #endregion

        #region ContactTitle

        [Test]
        public void ContactTitleIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.ContactTitle, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void ContactTitleIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.ContactTitle, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(20)]
        [TestCase(30)]
        public void ContactTitleIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.ContactTitle, new string('a', stringLength));
        }

        [TestCase(31)]
        public void ContactTitleIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.ContactTitle, new string('a', stringLength));
        }

        #endregion

        #region Address

        [Test]
        public void AddressIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Address, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        public void AddressIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Address, new string('a', stringLength));
        }

        [TestCase(5)]
        [TestCase(20)]
        [TestCase(60)]
        public void AddressIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Address, new string('a', stringLength));
        }

        [TestCase(61)]
        public void AddressIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Address, new string('a', stringLength));
        }

        #endregion

        #region City

        [Test]
        public void CityIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.City, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void CityIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.City, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(15)]
        public void CityIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.City, new string('a', stringLength));
        }

        [TestCase(16)]
        public void CityIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.City, new string('a', stringLength));
        }

        #endregion

        #region Region

        [Test]
        public void RegionIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Region, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void RegionIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Region, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(15)]
        public void RegionIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Region, new string('a', stringLength));
        }

        [TestCase(16)]
        public void RegionIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Region, new string('a', stringLength));
        }

        #endregion

        #region Country

        [Test]
        public void CountryIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Country, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void CountryIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Country, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(15)]
        public void CountryIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Country, new string('a', stringLength));
        }

        [TestCase(16)]
        public void CountryIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Country, new string('a', stringLength));
        }

        #endregion

        #region PostalCode

        [Test]
        public void PostalCodeIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.PostalCode, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void PostalCodeIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.PostalCode, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(10)]
        public void PostalCodeIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.PostalCode, new string('a', stringLength));
        }

        [TestCase(11)]
        public void PostalCodeIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.PostalCode, new string('a', stringLength));
        }

        #endregion

        #region Phone

        [Test]
        public void PhoneIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Phone, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void PhoneIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Phone, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(24)]
        public void PhoneIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Phone, new string('a', stringLength));
        }

        [TestCase(25)]
        public void PhoneIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Phone, new string('a', stringLength));
        }

        #endregion

        #region Fax

        [Test]
        public void FaxIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Fax, null as string);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void FaxIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Fax, new string('a', stringLength));
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(24)]
        public void FaxIsInAllowedRange_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.Fax, new string('a', stringLength));
        }

        [TestCase(25)]
        public void FaxIsLongerThanMaxAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.Fax, new string('a', stringLength));
        }

        #endregion

        #region HomePage

        [Test]
        public void HomePageIsNull_ShouldNotHaveError()
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.HomePage, null as string);
        }

        [TestCase(0)]
        [TestCase(3)]
        public void HomePageIsLessThanMinAllowed_ShouldHaveError(int stringLength)
        {
            _validator.ShouldHaveValidationErrorFor(supplier => supplier.HomePage, new string('a', stringLength));
        }

        [TestCase(4)]
        [TestCase(50)]
        public void HomePageIsGreaterThanMinAllowed_ShouldNotHaveError(int stringLength)
        {
            _validator.ShouldNotHaveValidationErrorFor(supplier => supplier.HomePage, new string('a', stringLength));
        }

        #endregion

    }
}
