using System;

namespace scooterRental.Exceptions
{
    public class NegativePriceExceptions : SystemException
    {
        public NegativePriceExceptions() : base("Price is negative")
        {
        }
    }
}