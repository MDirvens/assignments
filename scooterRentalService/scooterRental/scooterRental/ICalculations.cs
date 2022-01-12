using System.Collections.Generic;

namespace scooterRental
{
    public interface ICalculations
    {
        public decimal CalculateRentalPayment(RentedData scooter);

        public decimal CalculateAllYearsIncludeNotCompletedRentalsTrue(List<RentedData> rentedScooters);

        public decimal CalculateAllYearsIncludeNotCompletedRentalsFalse(List<RentedData> rentedScooters);

        public decimal CalculateYearIncludeNotCompletedRentalsFalse(List<RentedData> rentedScooters, int year);

        public decimal CalculateYearIncludeNotCompletedRentalsTrue(List<RentedData> rentedScooters, int year);
    }
}
