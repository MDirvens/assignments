using System;

namespace scooterRental.Exceptions
{
    public class NotRentedScooterException : SystemException
    {
        public NotRentedScooterException() : base("Scooter is not rented")
        {

        }
    }
}
