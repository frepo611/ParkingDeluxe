
namespace ParkingDeluxe;

public static class Utilities
{
    internal static Vehicle GetRandomVehicle()
    {
        int vehicleType = Random.Shared.Next(3);
        switch (vehicleType)
        {
            case 0:
                return GetRandomMC();
            case 1:
                return GetRandomCar();
            case 2:
                return GetRandomBus();
            default:
                throw new InvalidOperationException("Unexpected vehicle type"); // Fallback for safety
        }
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
    public static double GetElapsedMinutes(this ITimed timedObject) // an extension method, det är väl nice
    {
        return timedObject.ElapsedTime().TotalMinutes;
    }
}