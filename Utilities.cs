﻿namespace ParkingDeluxe;

public static class Utilities
{
    internal static Vehicle GetRandomVehicle()
    {
        int vehicleType = Random.Shared.Next(3);
        return vehicleType switch
        {
            0 => GetRandomMC(),
            1 => GetRandomCar(),
            2 => GetRandomBus(),
            _ => throw new InvalidOperationException("Unexpected vehicle type"),
        };
    }

    private static Bus GetRandomBus()
    {
        int passengers = Random.Shared.Next(3, 10) * 2;
        return new Bus(RegistrationNumber.GetRandom(), GetRandomColor(), passengers);
    }

    private static Car GetRandomCar()
    {
        bool isElectric = Random.Shared.Next(0, 2) > 0;
        return new Car(RegistrationNumber.GetRandom(), GetRandomColor(), isElectric);
    }

    private static MC GetRandomMC()
    {
        return new MC(RegistrationNumber.GetRandom(), GetRandomColor(), GetRandomMcBrand());
    }

    private static string GetRandomColor()
    {
        string[] colors = Colors.AvailableColors;
        return colors[Random.Shared.Next(colors.Length)];
    }

    private static string GetRandomMcBrand()
    {
        string[] mcBrands = McBrands.AvailableBrands;
        return mcBrands[Random.Shared.Next(mcBrands.Length)];
    }

    public static Vehicle CheckoutRandomVehicle(ParkingSpace parking)
    {
            int randomIndex = Random.Shared.Next(parking.ParkedVehicles.Count);
            return parking.ParkedVehicles.ElementAt(randomIndex).Value;
    }

    public static int GetElapsedMinutes(this ITimed timedObject) // an extension method, det är väl nice
    {
        return (int)Math.Ceiling(timedObject.ElapsedTime().TotalMinutes);
    }
}