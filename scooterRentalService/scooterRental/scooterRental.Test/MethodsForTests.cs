using System;
using System.Collections.Generic;

namespace scooterRental.Tests
{
    public class MethodsForTests
    {
        private List<RentedData> _rentedScooters;
        private readonly string _id1 = "1";
        private string _id2 = "87";
        private decimal _price1 = 0.20m;
        private decimal _price2 = 0.10m;
        private DateTime _normalStartTime = DateTime.UtcNow;
        private DateTime _startTime = new(2020, 12, 31, 23, 58, 00);
        private DateTime _endTime = new(2022, 1, 1, 0, 1, 00);

        public MethodsForTests(List<RentedData> rentedScooters)
        {
            _rentedScooters = rentedScooters;
        }

        internal List<RentedData> AddNoEndTimeStartTimeNegativeMinutes100()
        {
            _rentedScooters.Add(new RentedData()
            {
                Id = _id1,
                Price = _price1,
                StartRentTime = _normalStartTime.AddMinutes(-100),
            });
            return _rentedScooters;
        }

        internal List<RentedData> AddNoEndTimeStartTimeNegativeMinutes10()
        {
            _rentedScooters.Add(new RentedData()
            {
                Id = _id2,
                Price = _price2,
                StartRentTime = _normalStartTime.AddMinutes(-10),
            });
            return _rentedScooters;
        }

        internal List<RentedData> AddNoEndTimeStartTimeNegativeYears2()
        {
            _rentedScooters.Add(new RentedData()
            {
                Id = _id1,
                Price = _price1,
                StartRentTime = _normalStartTime.AddYears(-2),
            });
            return _rentedScooters;
        }

        internal List<RentedData> AddEndTimeStartTimeYears1()
        {
            _rentedScooters.Add(new RentedData()
            {
                Id = _id1,
                Price = _price1,
                StartRentTime = _startTime.AddYears(1),
                EndRentTime = _endTime
            });
            return _rentedScooters;
        }

        internal List<RentedData> AddEndTimeNegativeYears1StartTime()
        {
            _rentedScooters.Add(new RentedData()
            {
                Id = _id1,
                Price = _price1,
                StartRentTime = _startTime,
                EndRentTime = _endTime.AddYears(-1)
            });
            return _rentedScooters;
        }
    }
}
