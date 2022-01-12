using System;

namespace scooterRental.Exceptions
{
    public class NotExistingScooterException : SystemException
    {
        public NotExistingScooterException() : base("Not existing scooter")
        {
        }
    }
}
