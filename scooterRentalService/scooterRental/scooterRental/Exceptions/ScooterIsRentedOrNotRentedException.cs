using System;

namespace scooterRental.Exceptions
{
    public class ScooterIsRentedOrNotRentedException : SystemException
    {
        public ScooterIsRentedOrNotRentedException(string message) : base(message)
        {
        }
    }
}
