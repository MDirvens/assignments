using System;

namespace scooterRental.Exceptions
{
    public class InvalidIdException : SystemException
    {
        public InvalidIdException() : base("Invalid id input")
        {
        }
    }
}
