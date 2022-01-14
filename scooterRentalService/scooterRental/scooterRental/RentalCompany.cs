using System;
using System.Collections.Generic;
using System.Linq;
using scooterRental.Exceptions;

namespace scooterRental
{
    public class RentalCompany : IRentalCompany
    {
        private IScooterService _service;
        private List<RentedData> _rentedScooters;
        private readonly ICalculations _calculations;
        

        public RentalCompany(string name, IScooterService service, List<RentedData> rentedScooters, ICalculations calculations)
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

            ThrowExceptions(id, "Scooter is already rented", scooter.IsRented);

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

            ThrowExceptions(id, "Scooter is not rented", !scooter.IsRented);
            rented = _rentedScooters.First(s => s.Id == id && !s.EndRentTime.HasValue);
            rented.EndRentTime = DateTime.UtcNow;

            return _calculations.CalculateRentalPayment(rented);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return _calculations.CalculateIncome(year, includeNotCompletedRentals, _rentedScooters);
        }

        public void ThrowExceptions(string id, string message, bool isRented)
        {
            var scooter = _service.GetScooterById(id);

            if (isRented)
            {
                throw new ScooterIsRentedOrNotRentedException(message);
            }

            scooter.IsRented = !scooter.IsRented;
            //
        }
    }
}
