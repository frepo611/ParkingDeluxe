using System.Globalization;

namespace ParkingDeluxe;

internal class Program
{
    static void Main(string[] args)
    {
        CultureInfo swedishCulture = new CultureInfo("sv-SE");
        CultureInfo.DefaultThreadCurrentCulture = swedishCulture;
        CultureInfo.DefaultThreadCurrentUICulture = swedishCulture;
       
        ParkingSpace garage = new(15);
        ConsoleUI UI = new ConsoleUI(garage);
        //UI.Start();
        UI.StartSim();


    }
}
