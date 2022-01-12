using System;

namespace scooterRental
{
    public class RentedData
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public DateTime StartRentTime { get; set; }

        public DateTime? EndRentTime { get; set; }

    }
}
