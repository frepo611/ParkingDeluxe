namespace ParkingDeluxe;

internal class Program
{
    static void Main(string[] args)
    {

        ParkingSpace garage = new(15);
        ConsoleUI UI = new ConsoleUI(garage);
        UI.Start();
        
    }
}
