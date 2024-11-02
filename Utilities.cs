
namespace ParkingDeluxe;

public class Utilities
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
        bool isElectric = (Random.Shared.Next(0, 2) > 0) ? true : false;
        return new Car(RegistrationNumber.GetRandom(), GetRandomColor(), isElectric);
    }

    private static MC GetRandomMC()
    {
        return new MC(RegistrationNumber.GetRandom(), GetRandomColor(), GetRandomMcBrand());
    }

    private static string GetRandomColor()
    {
        string[] ColorsAsArray = Enum.GetNames(typeof(Colors));
        return ColorsAsArray[Random.Shared.Next(ColorsAsArray.Length)];
    }
    private static string GetRandomMcBrand()
    {
        string[] BrandsAsArray = Enum.GetNames(typeof(McBrands));
        return BrandsAsArray[Random.Shared.Next(BrandsAsArray.Length)];
    }
    private enum Colors
    {
        Black,
        White,
        Red,
        Green,
        Yellow,
        Blue,
        Silver
    }
    private enum McBrands
    {
        HarleyDavidson,
        BMW,
        Yamaha,
        Suzuki,
        Aprilia,
        Honda
    }
    public static void CheckoutRandomVehicle(ParkingSpace parking)
    {
        bool vehicleCheckedOut = false;
        while (!vehicleCheckedOut)
        {
            int randomSpot = Random.Shared.Next(parking.ParkingSpots.Count);
            vehicleCheckedOut = parking.Checkout(parking.ParkingSpots[randomSpot].OccupiedBy);
        }
    }
}