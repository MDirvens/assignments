using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using scooterRental.Exceptions;

namespace scooterRental.Tests
{
    [TestClass]
    public class CalculationTests
    {
        private List<RentedData> _rentedScooters = new();
        private ICalculations _target;
        private MethodsForTests _methods;
        private string _id1 = "1";
        private string _id2 = "87";
        private int year = 2021;
        private int _YearsInMinutes = 525600;
        private decimal _price1 = 0.20m;
        private decimal _price2 = 0.10m;
        private DateTime _normalTime = DateTime.UtcNow;

        [TestInitialize]
        public void Setup()
        {
            _methods = new MethodsForTests();
            _target = new Calculations();

            _rentedScooters.Add(new RentedData()
            {
                Id = _id1,
                Price = _price1,
                StartRentTime = _normalTime.AddMinutes(-100),
            });
            _rentedScooters.Add(new RentedData()
            {
                Id = _id2,
                Price = _price2,
                StartRentTime = _normalTime.AddMinutes(-10),
                EndRentTime = _normalTime
            });
        }

        [TestMethod]
        public void CalculateRentalPayment_YearNullIncIncludeNotCompletedRentalsFalse_NegativePriceExceptions()
        {
            ////Arrange
            _methods.ChangeEndtime(_rentedScooters, _id1, _YearsInMinutes, -_YearsInMinutes);
            var rented = _rentedScooters.First(s => s.Id == _id1);

            ////Assert
            Assert.ThrowsException<NegativePriceExceptions>(() => _target.CalculateRentalPayment(rented));
        }

        [TestMethod]
        public void CalculateAllYearsIncludeNotCompletedRentalsFalse_YearNullIncIncludeNotCompletedRentalsFalse_Income1()
        {
            //Act
            var result = _target.CalculateAllYearsIncludeNotCompletedRentalsFalse(_rentedScooters);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateAllYearsIncludeNotCompletedRentalsTrue_YearNullIncludeNotCompletedRentalsTrue_Income21()
        {
           //Act
            var result = _target.CalculateAllYearsIncludeNotCompletedRentalsTrue(_rentedScooters);

            //Assert
            Assert.AreEqual(21.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseSameYear_Income1()
        {
            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseDifferentYears_Income52560()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id2, 0, 0);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(52560.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseReturnedDifferentYears_Income0Point20()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id2, _YearsInMinutes, 0);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.20m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseStartedDifferentYears_Income0Point10()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id2, 0, -_YearsInMinutes);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.10m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueSameYear_Income21()
        {
            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(21.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueDifferentYears_Income105121()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id1, 0, 0);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(105121.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueReturnedDifferentYears_Income1point40()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id1, _YearsInMinutes, 0);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(1.40m, result);
        }

        [TestMethod]
        public void
            CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueStartedDifferentYears_Income1point20()
        {
            //Arrange
            _methods.ChangeEndtime(_rentedScooters, _id1, 0, -_YearsInMinutes);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(1.20m, result);
        }
    }
}
