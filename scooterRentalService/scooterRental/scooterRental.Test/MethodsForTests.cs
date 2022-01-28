using System;
using System.Collections.Generic;
using System.Linq;

namespace scooterRental.Tests
{
    public class MethodsForTests
    {
        private DateTime _startTime = new(2020, 12, 31, 23, 58, 00);
        private DateTime _endTime = new(2022, 1, 1, 0, 1, 00);
        RentedData rented;

        public List<RentedData> ChangeEndtime(List<RentedData> rentedScooters, string id,int toChangeStartValue, int toChangeEndValue)
        {
            rented = rentedScooters.First(s => s.Id == id);
            rented.StartRentTime = _startTime.AddMinutes(toChangeStartValue);
            rented.EndRentTime = _endTime.AddMinutes(toChangeEndValue);
            return rentedScooters;
        }
    }
}
