using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace scooterRental.Tests
{
    [TestClass]
    public class CalculationTests
    {
        private ICalculations _calculations = new Calculations();
        private List<RentedData> _rentedScooters = new();
        private ScooterService _service = new();
        private Calculations _target;
        private RentalCompany _rentalCompany;
        private MethodsForTests _methods;
        private string _name = "Company";
        private string _id1 = "1";
        private string _id2 = "87";
        private int year = 2021;
        private decimal _price1 = 0.20m;
        private decimal _price2 = 0.10m;

        [TestInitialize]
        public void Setup()
        {
            _rentalCompany =new RentalCompany(_name, _service, _rentedScooters, _calculations);
            _methods = new MethodsForTests(_rentedScooters);
            _target = new Calculations();
            _service.AddScooter(_id1, _price1);
            _service.AddScooter(_id2, _price2);
        }

        [TestMethod]
        public void CalculateAllYearsIncludeNotCompletedRentalsFalse_YearNullIncIncludeNotCompletedRentalsFalse_Income1()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes100();

            _service.GetScooterById(_id2).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes10();
            _rentalCompany.EndRent(_id2);

            //Act
            var result = _target.CalculateAllYearsIncludeNotCompletedRentalsFalse(_rentedScooters);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateAllYearsIncludeNotCompletedRentalsTrue_YearNullIncludeNotCompletedRentalsTrue_Income21()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes100();

            _service.GetScooterById(_id2).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes10();
            _rentalCompany.EndRent(_id2);

            //Act
            var result = _target.CalculateAllYearsIncludeNotCompletedRentalsTrue(_rentedScooters);

            //Assert
            Assert.AreEqual(21.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseSameYear_Income1()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes100();

            _service.GetScooterById(_id2).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes10();
            _rentalCompany.EndRent(_id2);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseDifferentYears_Income105120()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeYears2();
            _rentalCompany.EndRent(_id1);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(105120.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseReturnedDifferentYears_Income0Point04()
        {
            //Arrange
            _rentedScooters = _methods.AddEndTimeStartTimeYears1();

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.40m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsFalse_YearNotNullIncludeNotCompletedRentalsFalseStartedDifferentYears_Income0Point20()
        {
            //Arrange
            _rentedScooters = _methods.AddEndTimeNegativeYears1StartTime();

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.20m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueSameYear_Income21()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes100();

            _service.GetScooterById(_id2).IsRented = true;
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeMinutes10();
            _rentalCompany.EndRent(_id2);

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(21.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueDifferentYears_Income105120()
        {
            //Arrange
            _rentedScooters = _methods.AddNoEndTimeStartTimeNegativeYears2();

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(105120.00m, result);
        }

        [TestMethod]
        public void CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueReturnedDifferentYears_Income0point40()
        {
            //Arrange
            _rentedScooters = _methods.AddEndTimeStartTimeYears1();

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.40m, result); ;
        }

        [TestMethod]
        public void
            CalculateYearIncludeNotCompletedRentalsTrue_YearNotNullIncludeNotCompletedRentalsTrueStartedDifferentYears_Income0point20()
        {
            //Arrange
            _rentedScooters = _methods.AddEndTimeNegativeYears1StartTime();

            //Act
            var result = _target.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, year);

            //Assert
            Assert.AreEqual(0.20m, result);
        }
    }
}
