using System;
using System.Collections.Generic;
using System.Linq;
using scooterRental.Exceptions;

namespace scooterRental
{
    public class Calculations : ICalculations
    {
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals, List<RentedData> rentedScooters)
        {
            decimal yearIncome = 0m;

            if (year.ToString().Length != 4 && year != null)
            {
                throw new InvalidYearException();
            }

            if (year == null && includeNotCompletedRentals)
            {
                yearIncome = CalculateAllYearsIncludeNotCompletedRentalsTrue(rentedScooters);
            }
            else if (year == null && !includeNotCompletedRentals)
            {
                yearIncome = CalculateAllYearsIncludeNotCompletedRentalsFalse(rentedScooters);
            }
            else if (year != null && !includeNotCompletedRentals)
            {
                yearIncome = CalculateYearIncludeNotCompletedRentalsFalse(rentedScooters, (int)year);
            }
            else if (year != null && includeNotCompletedRentals)
            {
                yearIncome = CalculateYearIncludeNotCompletedRentalsTrue(rentedScooters, (int)year);
            }

            return yearIncome;
        }

        public decimal CalculateRentalPayment(RentedData scooter)
        {
            var time = scooter.EndRentTime - scooter.StartRentTime;
            var payment = (decimal) time.Value.TotalMinutes * scooter.Price;

            if (payment < 0)
            {
                throw new NegativePriceExceptions();
            }
            
            return Math.Round(payment, 4);
        }

        public decimal CalculateAllYearsIncludeNotCompletedRentalsFalse(List<RentedData> rentedScooters)
        {
            var income = rentedScooters.Where(s => s.EndRentTime != null).Sum(s => CalculateRentalPayment(s));
            return Math.Round(income, 2);
        }

        public decimal CalculateAllYearsIncludeNotCompletedRentalsTrue(List<RentedData> rentedScooters)
        {
            var incomeEnded = rentedScooters.Where(s => s.EndRentTime != null).Sum(s => CalculateRentalPayment(s));

            decimal incomeContinue = 0;
            foreach (var s in rentedScooters)
            {
                if (s.EndRentTime == null)
                {
                    s.EndRentTime = DateTime.UtcNow;
                    incomeContinue += CalculateRentalPayment(s);
                }
            }

            return Math.Round(incomeEnded + incomeContinue,2);
        }

        public decimal CalculateYearIncludeNotCompletedRentalsFalse(List<RentedData> rentedScooters, int year)
        {
            decimal income = 0;
            
            foreach (var s in rentedScooters)
            {
                if (s.EndRentTime.HasValue && s.StartRentTime.Year == s.EndRentTime.Value.Year)
                {
                    income += CalculateRentalPayment(s);
                }
                else if (s.EndRentTime.HasValue && s.EndRentTime.Value.Year > year && s.StartRentTime.Year < year)
                {
                    income += CountIncomeInOneYear(s);
                }
                else if (s.EndRentTime.HasValue && s.EndRentTime.Value.Year == year && s.StartRentTime.Year < year)
                {
                    income += CountIncomeDifferentStartTime(s, year);
                }
                else if  (s.EndRentTime.HasValue && s.EndRentTime.Value.Year > year && s.StartRentTime.Year == year)
                {
                    income += CountIncomeDifferentEndTime(s, year);
                }
            }

            return Math.Round(income,2);
        }

        public decimal CalculateYearIncludeNotCompletedRentalsTrue(List<RentedData> rentedScooters, int year)
        {
            decimal income = 0;
            foreach (var s in rentedScooters)
            {
                if (!s.EndRentTime.HasValue )
                {
                    s.EndRentTime = DateTime.UtcNow;
                }
            }

            foreach (var s in rentedScooters)
            {
                if (s.StartRentTime.Year == s.EndRentTime.Value.Year)
                {
                    income += CalculateRentalPayment(s);
                }
                else if (s.EndRentTime.Value.Year > year && s.StartRentTime.Year < year)
                {
                    income += CountIncomeInOneYear(s);
                }
                else if (s.EndRentTime.Value.Year == year && s.StartRentTime.Year < year)
                {
                    income += CountIncomeDifferentStartTime(s, year);
                }
                else if (s.EndRentTime.Value.Year > year && s.StartRentTime.Year == year)
                {
                    income += CountIncomeDifferentEndTime(s, year);
                }
            }

            return Math.Round(income, 2);
        }

        private decimal CountIncomeInOneYear(RentedData s)
        {
            var minutesInYear = 525600;

            return Math.Round(minutesInYear * s.Price, 4);
        }

        private decimal CountIncomeDifferentStartTime(RentedData s, int year)
        {
            var date = new DateTime(year, 1, 1);
            var time = s.EndRentTime - date;

            return Math.Round((decimal)time.Value.TotalMinutes * s.Price, 4);
        }

        private decimal CountIncomeDifferentEndTime(RentedData s, int year)
        {
            var date = new DateTime(year, 12, 31, 23, 59, 59);
            var time = date - s.StartRentTime;
            
            return Math.Round(((decimal) time.TotalMinutes ) * s.Price, 4);
        }
    }
}
