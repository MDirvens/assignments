using System;
using System.Collections.Generic;
using System.Linq;
using scooterRental.Exceptions;

namespace scooterRental
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute < 0)
            {
                throw new InvalidPriceException();
            }

            if (String.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            if (_scooters.Any(s => s.Id == id))
            {
                throw new ScooterAlreadyExistException();
            }

            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (_scooters.All(s =>s.Id != id))
            {
                throw new NotExistingScooterException();
            }

            _scooters.Remove(_scooters.First(s => s.Id == id));
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(s => s.Id == scooterId);

            if (scooter == null)
            {
                throw new NotExistingScooterException();
            }

            return scooter;
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }
    }
}
