using System;

namespace scooterRental.Exceptions
{
    public class ScooterAlreadyExistException : SystemException
    {
        public ScooterAlreadyExistException() : base("Scooter already Exist")
        {
        }
    }
}
