using System;
using System.Collections.Generic;
using System.Linq;
using scooterRental.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace scooterRental.Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private RentalCompany _target;
        private string _name = "Company";
        private ScooterService _service = new();
        private List<RentedData> _rentedScooters = new();
        private ICalculations _calculations = new Calculations();
        private string _id1 = "1";
        private string _id2 = "87";
        private string _id3 = "12";
        private int _year = 2022;
        private decimal _price1 = 0.20m;
        private decimal _price2 = 0.10m;
        private DateTime _normalTime = DateTime.UtcNow;

        [TestInitialize]
        public void Setup()
        {
            _target = new RentalCompany(_name, _service, _rentedScooters, _calculations);
            _service.AddScooter(_id1, _price1);
            _service.AddScooter(_id2, _price2);

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
        public void Name_CreateCompany_GetSameNameBack()
        {
            //Assert
            Assert.AreEqual(_name, _target.Name);
        }

        [TestMethod]
        public void Name_CreateCompany_NoNameException()
        {
            //Arrange
            string name = null;

            //Assert
            Assert.ThrowsException<NoNameException>(() =>
                _target = new RentalCompany(name, _service, _rentedScooters, _calculations));
        }

        [TestMethod]
        public void StartRent_AddScooter_ScooterIsRented()
        {
            //Act
            _target.StartRent(_id1);

            //Assert
            Assert.IsTrue(_service.GetScooterById(_id1).IsRented);
        }

        [TestMethod]
        public void StartRent_RentRentedScooter_ScooterIsRentedException()
        {
            //Act
            _target.StartRent(_id1);

            //Assert
            Assert.ThrowsException<ScooterIsRentedOrNotRentedException>(() => _target.StartRent(_id1));
        }

        [TestMethod]
        public void StartRent_RentNotExistinScooter_NotExistingScooterException()
        {
            //Assert
            Assert.ThrowsException<NotExistingScooterException>(() => _target.StartRent(_id3));
        }

        [TestMethod]
        public void StartRent_RentScooter_Scooter_RentedScootersListUpdated()
        {
            //Act
            _target.StartRent(_id1);

            //Assert
            Assert.AreEqual(3, _rentedScooters.Count);
        }

        [TestMethod]
        public void EndRent_ReturnScooter_ScooterIsReturned()
        {
            //Arrange
            _target.StartRent(_id1);

            //Act
            var result = _target.EndRent(_id1);

            //Assert
            Assert.IsTrue(0 <= result);
        }

        [TestMethod]
        public void EndRent_ReturnNotRentedScooter_NotRentedScooterException()
        {
            //Assert
            Assert.ThrowsException<ScooterIsRentedOrNotRentedException>(() => _target.EndRent(_id1));
        }

        [TestMethod]
        public void EndRent_ReturnScooter_rentedScootersEndRentTimeAdded()
        {
            //Arrange
            _target.StartRent(_id1);
            var rented = _rentedScooters.First(s => s.Id == _id1 && !s.EndRentTime.HasValue);

            //Act
            _target.EndRent(_id1);

            //Assert
            Assert.IsTrue(rented.EndRentTime.HasValue);
        }

        [TestMethod]
        public void EndRent_ReturnScooter_ReturnPayment20()
        {
            //Arrange
            _service.GetScooterById(_id1).IsRented = true;

            //Act
            var result = _target.EndRent(_id1);

            //Assert
            Assert.AreEqual(20.00m, result);
        }

        [TestMethod]
        public void CalculateIncome_YearInvalid_InvalidYearException()
        {
            //Assert
            Assert.ThrowsException<InvalidYearException>(() => _target.CalculateIncome(45, true));
        }

        [TestMethod]
        public void CalculateIncome_YearNullIncludeNotCompletedRentalsTrue_Income21()
        {
            //Act
            var result = _target.CalculateIncome(null, true);

            //Assert
            Assert.AreEqual(21.00m, result);
        }

        [TestMethod]
        public void CalculateIncome_YearNullIncludeNotCompletedRentalsFalse_Income1()
        {
            //Act
            var result = _target.CalculateIncome(null, false);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateIncome_YearNotNullIncludeNotCompletedRentalsFalse_Income1()
        {
            //Act
            var result = _target.CalculateIncome(_year, false);

            //Assert
            Assert.AreEqual(1.00m, result);
        }

        [TestMethod]
        public void CalculateIncome_YearNotNullIncludeNotCompletedRentalsTrue_Income21()
        {
            //Act
            var result = _target.CalculateIncome(_year, true);

            //Assert
            Assert.AreEqual(21.00m, result);
        }
    }
}