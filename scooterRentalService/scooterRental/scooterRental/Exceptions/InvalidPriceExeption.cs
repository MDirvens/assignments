using System;

namespace scooterRental.Exceptions
{
    public class InvalidPriceException : SystemException
    {
        public InvalidPriceException() : base("Invalid price input")
        {
        }
    }
}
