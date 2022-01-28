using System;

namespace scooterRental.Exceptions
{
    public class InvalidYearException : SystemException
    {
        public InvalidYearException() : base("Invalid year")
        {
        }
    }
}
