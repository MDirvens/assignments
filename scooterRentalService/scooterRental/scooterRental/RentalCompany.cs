using System;
using System.Collections.Generic;
using System.Linq;
using scooterRental.Exceptions;

namespace scooterRental
{
    public class RentalCompany : IRentalCompany
    {
        private ScooterService _service;
        private List<RentedData> _rentedScooters;
        private readonly ICalculations _calculations;

        public RentalCompany(string name, ScooterService service, List<RentedData> rentedScooters, ICalculations calculations)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new NoNameException();
            }

            Name = name;
            _service = service;
            _rentedScooters = rentedScooters;
            _calculations = calculations;
        }

        public string Name { get; }

        public void StartRent(string id)
        {
            var scooter = _service.GetScooterById(id);

            if (scooter.IsRented)
            {
                throw new ScooterIsRentedException();
            }

            scooter.IsRented = true;

            _rentedScooters.Add(new RentedData()
            {
                Id = scooter.Id,
                Price = scooter.PricePerMinute,
                StartRentTime = DateTime.UtcNow,
            });
        }

        public decimal EndRent(string id)
        {
            RentedData rented;
            var scooter = _service.GetScooterById(id);

            if (!scooter.IsRented)
            {
                throw new NotRentedScooterException();
            }

            scooter.IsRented = false;
            rented = _rentedScooters.First(s => s.Id == id && !s.EndRentTime.HasValue);
            rented.EndRentTime = DateTime.UtcNow;

            return _calculations.CalculateRentalPayment(rented);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal yearIncome = 0m;

            if (year.ToString().Length != 4 && year != null)
            {
                throw new InvalidYearException();
            }

            if (year == null && includeNotCompletedRentals)
            {
                yearIncome = _calculations.CalculateAllYearsIncludeNotCompletedRentalsTrue(_rentedScooters);
            }
            else if (year == null && !includeNotCompletedRentals)
            {
                yearIncome = _calculations.CalculateAllYearsIncludeNotCompletedRentalsFalse(_rentedScooters);
            }
            else if (year != null && !includeNotCompletedRentals)
            {
                yearIncome = _calculations.CalculateYearIncludeNotCompletedRentalsFalse(_rentedScooters, (int)year);
            }
            else if (year != null && includeNotCompletedRentals)
            {
                yearIncome = _calculations.CalculateYearIncludeNotCompletedRentalsTrue(_rentedScooters, (int)year);
            }

            return yearIncome;
        }
    }
}
