using System;

namespace scooterRental.Exceptions
{
    public class ScooterIsRentedException : SystemException
    {
        public ScooterIsRentedException() : base("Scooter is already rented")
        {
        }
    }
}
