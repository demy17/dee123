using System;
using System.Collections.Generic;
using Bogus;

abstract class ParkingGarage
{
    public string Name { get; }
    public double MinimumFee { get; }
    public double AdditionalFeePerHour { get; }
    public double MaximumDailyCharge { get; }

    protected ParkingGarage(string name, double minimumFee, double additionalFeePerHour, double maximumDailyCharge)
    {
        Name = name;
        MinimumFee = minimumFee;
        AdditionalFeePerHour = additionalFeePerHour;
        MaximumDailyCharge = maximumDailyCharge;
    }

    public abstract double CalculateCharges(int hoursParked);
}

class Garage1 : ParkingGarage
{
    public Garage1() : base("Garage 1", 2.00, 0.50, 10.00) { }

    public override double CalculateCharges(int hoursParked)
    {
        return Math.Min(MaximumDailyCharge, MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour);
    }
}

class Garage2 : ParkingGarage
{
    public Garage2() : base("Garage 2", 2.00, 0.60, 10.00) { }

    public override double CalculateCharges(int hoursParked)
    {
        return Math.Min(MaximumDailyCharge, MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour);
    }
}

class Garage3 : ParkingGarage
{
    public Garage3() : base("Garage 3", 2.00, 0.75, 10.00) { }

    public override double CalculateCharges(int hoursParked)
    {
        return Math.Min(MaximumDailyCharge, MinimumFee + Math.Max(0, hoursParked - 3) * AdditionalFeePerHour);
    }
}

class Program
{
    static void Main()
    {
        List<ParkingGarage> garages = new List<ParkingGarage>
        {
            new Garage1(),
            new Garage2(),
            new Garage3()
        };

        Faker faker = new Faker();

        double totalReceipts = 0;

        foreach (var garage in garages)
        {
            double garageReceipts = 0;

            for (int i = 0; i < 5; i++) // Assuming 5 customers per garage for the example
            {
                int hoursParked = faker.Random.Int(1, 24);
                string registrationNumber = faker.Vehicle.Vin();

                double charge = garage.CalculateCharges(hoursParked);
                garageReceipts += charge;

                Console.WriteLine($"{registrationNumber} parked in {garage.Name} for {hoursParked} hours. Charge: €{charge:F2}");
            }

            Console.WriteLine($"{garage.Name} total receipts: €{garageReceipts:F2}\n");

            totalReceipts += garageReceipts;
        }

        Console.WriteLine($"Total receipts for all garages: €{totalReceipts:F2}");
    }
}
