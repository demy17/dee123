using System;
using System.Collections.Generic;

abstract class ParkingGarage
{
    public string Name { get; }
    protected decimal MinimumFee { get; }
    protected decimal AdditionalFeePerHour { get; }
    protected decimal MaximumDailyCharge { get; }

    protected ParkingGarage(string name, decimal minimumFee, decimal additionalFeePerHour, decimal maximumDailyCharge)
    {
        Name = name;
        MinimumFee = minimumFee;
        AdditionalFeePerHour = additionalFeePerHour;
        MaximumDailyCharge = maximumDailyCharge;
    }

    public abstract decimal CalculateCharges(int hoursParked);
}

class Garage1 : ParkingGarage
{
    public Garage1() : base("Garage 1", 2.00m, 0.50m, 10.00m) { }

    public override decimal CalculateCharges(int hoursParked)
    {
        decimal charge = MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour;
        return Math.Min(MaximumDailyCharge, charge);
    }
}

class Garage2 : ParkingGarage
{
    public Garage2() : base("Garage 2", 2.00m, 0.60m, 10.00m) { }

    public override decimal CalculateCharges(int hoursParked)
    {
        decimal charge = MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour;
        return Math.Min(MaximumDailyCharge, charge);
    }
}

class Garage3 : ParkingGarage
{
    public Garage3() : base("Garage 3", 2.00m, 0.75m, 10.00m) { }

    public override decimal CalculateCharges(int hoursParked)
    {
        decimal charge = MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour;
        return Math.Min(MaximumDailyCharge, charge);
    }
}

class Program
{
    static void Main()
    {

        Console.WriteLine("Student Name: Adejare Daniel");
        Console.WriteLine("Student ID: 24080\n");

        List<ParkingGarage> garages = new List<ParkingGarage>
        {
            new Garage1(),
            new Garage2(),
            new Garage3()
        };

        decimal totalReceipts = 0;

        foreach (var garage in garages)
        {
            Console.WriteLine($"{garage.Name} Parking Charges:");
            decimal garageReceipts = CalculateAndDisplayCharges(garage);
            Console.WriteLine($"Total receipts for {garage.Name}: €{garageReceipts:F2}\n");
            totalReceipts += garageReceipts;
        }

        Console.WriteLine($"Total receipts for all garages: €{totalReceipts:F2}");
        Console.ReadLine();
    }

    static decimal CalculateAndDisplayCharges(ParkingGarage garage)
    {
        int lessThan3HoursCount = 0;
        int exactly3HoursCount = 0;
        int moreThan3HoursCount = 0;
        decimal runningTotal = 0;

        for (int i = 0; i < 10; i++)
        {
            string registrationNumber = GenerateRandomRegistrationNumber();
            Console.Write($"Enter hours parked for customer {registrationNumber}: ");
            int hoursParked = int.Parse(Console.ReadLine());

            decimal charge = garage.CalculateCharges(hoursParked);
            runningTotal += charge;
            Console.WriteLine($"Parking charge for customer {registrationNumber}: €{charge:F2}");
            Console.WriteLine($"Running total for {garage.Name}: €{runningTotal:F2}");

            if (hoursParked < 3)
            {
                lessThan3HoursCount++;
            }
            else if (hoursParked == 3)
            {
                exactly3HoursCount++;
            }
            else
            {
                moreThan3HoursCount++;
            }
        }

        Console.WriteLine($"Less than 3 hours: {lessThan3HoursCount} cars");
        Console.WriteLine($"Exactly 3 hours: {exactly3HoursCount} cars");
        Console.WriteLine($"More than 3 hours: {moreThan3HoursCount} cars");

        return runningTotal;
    }

    static string GenerateRandomRegistrationNumber()
    {
        Random random = new Random();
        return $"ABC{random.Next(100, 1000)}";
    }
}
