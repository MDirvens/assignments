using System;
using System.Collections.Generic;
using scooterRental;


namespace ConsoleApp1
{
    public class Program
    {
        private RentalCompany _target;
        private string _name = "Company";
        private ScooterService _service = new();
        private List<RentedData> _rentedScooters = new();
        private ICalculations _calculations = new Calculations();

        int year = 2022;
        string id = "78";
        decimal price = 0.20m;
        _service.AddScooter(id, price);
        _service.GetScooterById(id).IsRented = true;
        var startTime = DateTime.UtcNow.AddMinutes(-100);
        _rentedScooters.Add(new RentedData()
        {
            Id = id,
            Price = price,
            StartRentTime = startTime,
        });

        string id2 = "8";
        decimal price2 = 0.20m;
        _service.AddScooter(id2, price2);
        _service.GetScooterById(id2).IsRented = true;
        var startTime2 = DateTime.UtcNow.AddMinutes(-100);
        _rentedScooters.Add(new RentedData()
        {
            Id = id2,
            Price = price2,
            StartRentTime = startTime2,
        });
        _target.EndRent(id2);

        //Act
        result = _target.CalculateIncome(year, false);
        
        public static void Main(string[] args)
        {
            Console.WriteLine(id);

        }
    }
}
