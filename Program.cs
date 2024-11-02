namespace ParkingDeluxe;

internal class Program
{
    static void Main(string[] args)
    {

        ParkingSpace parking = new(15);

        bool runAgain = true;
        while (runAgain)
        {
            Console.WriteLine("P or C");
            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            switch (keyPress.Key)
            {
                case ConsoleKey.P:
                    parking.Park(Utilities.GetRandomVehicle());
                    break;
                case ConsoleKey.C:
                    Utilities.CheckoutRandomVehicle(parking);
                    break;
                case ConsoleKey.Q:
                    runAgain = false;
                    break;
            }
            Console.Clear();
            for (int i = 0; i < parking.Count; i++)
            {
                {
                    Console.WriteLine($"{parking.ParkingSpots[i].ID}: {parking.ParkingSpots[i].OccupiedBy}");
                }
            }
        }
    }
}
