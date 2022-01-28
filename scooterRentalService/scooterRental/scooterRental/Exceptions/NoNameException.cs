using System;

namespace scooterRental.Exceptions
{
    public class NoNameException : SystemException
    {
        public NoNameException() : base("No company name")
        {
        }
    }
}
