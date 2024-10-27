using System.ComponentModel.DataAnnotations;

namespace ParkingDeluxe;

internal class Program
{
    static void Main(string[] args)
    {
        var car = new Car(new RegistrationNumber(123,"HUO"),"Röd", false);
        var mc = new MC(new RegistrationNumber(111, "LOL"), "Röd", "YAMAHA");
        var bus = new Bus(new RegistrationNumber(100, "FAK"), "Röd", 8);
        List<Vehicle> vehicles = [car, mc, bus];

        foreach (var vehicle in vehicles)
        {
            string misc = vehicle switch
            {
                Car c => c.IsElectric ? "Elbil" : "Ingen elbil",
                MC m => m.Brand,
                Bus b => $"{b.NoOfPassengers} passagerare",
                _ => throw new InvalidOperationException("Unexpected vehicle type")
            };

            Console.WriteLine($"{vehicle.RegistrationNumber.RegNumber} {vehicle.Color} {vehicle.GetType().Name} {misc}");
        }
    }

}
