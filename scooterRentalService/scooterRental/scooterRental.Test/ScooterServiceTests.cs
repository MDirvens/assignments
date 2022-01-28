using Microsoft.VisualStudio.TestTools.UnitTesting;
using scooterRental.Exceptions;

namespace scooterRental.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private ScooterService _target;
        private string _id = "1";
        private decimal _price = 1m;
        private Scooter _scooter1 = new Scooter("1", 1m);
        private Scooter _scooter2 = new Scooter("67", 1m);
        private Scooter _scooter3 = new Scooter("98", 1m);

        [TestInitialize]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [TestMethod]
        public void AddScooter_1_050_ScooterAdded()
        {
            //Act
            _target.AddScooter(_id, _price);

            //Assert
            Assert.AreEqual(1,_target.GetScooters().Count);
        }

        [TestMethod]
        public void AddScooter_1_Negative050_InvalidPriceException()
        {
            //Assert
            Assert.ThrowsException<InvalidPriceException>(() => _target.AddScooter(_id, -_price));
        }

        [TestMethod]
        public void AddScooter_NullId_InvalidIdException()
        {
            //Assert
            Assert.ThrowsException<InvalidIdException>(() => _target.AddScooter(null, _price));
        }

        [TestMethod]
        public void AddScooter_1_050_ScooterAlreadyExistException()
        {
            //Arrange
            _target.AddScooter(_id, _price);

            //Assert
            Assert.ThrowsException<ScooterAlreadyExistException>(() => _target.AddScooter(_id, _price));
        }

        [TestMethod]
        public void RemoveScooter_1_050_ScooterRemoved()
        {
            //Arrange
            _target.AddScooter(_id, _price);

            //Act
            _target.RemoveScooter(_id);

            //Assert
            Assert.ThrowsException<NotExistingScooterException>(() => _target.GetScooterById(_id));
        }

        [TestMethod]
        public void RemoveScooter_NotAdded_Exception()
        {
            //Assert
            Assert.ThrowsException<NotExistingScooterException>(() => _target.RemoveScooter(_id));
        }

        [TestMethod]
        public void GetScooters_AddScooters_ShoudFail()
        {
            //Arrange
            var scooter = _target.GetScooters();
            scooter.Add(_scooter1);
            scooter.Add(_scooter2);
            scooter.Add(_scooter3);

            //Act
            bool addedScooters = _target.GetScooters().Count == 3;

            //Assert
            Assert.IsFalse(addedScooters);
        }

        [TestMethod]
        public void GetScooterById_AddScooters_GetScooter()
        {
            //Arrange
            Scooter scooter;
            _target.AddScooter(_scooter1.Id, _scooter1.PricePerMinute);
            _target.AddScooter(_scooter2.Id, _scooter2.PricePerMinute);
            _target.AddScooter(_scooter3.Id, _scooter3.PricePerMinute);
            var excepted = _scooter1;
            
            //Act
            scooter = _target.GetScooterById(_scooter1.Id);
            
            //Assert
            Assert.AreEqual(excepted.Id, scooter.Id);
        }
    }
}
