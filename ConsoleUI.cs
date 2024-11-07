
using System.ComponentModel.Design;

namespace ParkingDeluxe
{
    internal class ConsoleUI
    {
        private ParkingSpace _garage;

        public ConsoleUI(ParkingSpace garage)
        {
            _garage = garage;
        }

        internal void Start()
        {
            bool runAgain = true;
            while (runAgain)
            {
                Console.WriteLine("P or C");
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                switch (keyPress.Key)
                {
                    case ConsoleKey.P:
                        _garage.Park(Utilities.GetRandomVehicle());
                        break;
                    case ConsoleKey.C:
                        Utilities.CheckoutRandomVehicle(_garage);
                        break;
                    case ConsoleKey.Q:
                        runAgain = false;
                        break;
                }
                Console.Clear();
                for (int i = 0; i < _garage.Count; i++)
                {
                    {
                        Console.WriteLine($"{_garage.ParkingSpots[i].ID}: {_garage.ParkingSpots[i].OccupyingVechicle}");
                    }
                }
                ShowMainMenu();
                ListParkedVehicles();
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("1. Parkera ett fordon");
            Console.WriteLine("1. Checka ut ett fordon");
            Console.WriteLine("Avsluta");

        }

        internal void ListParkedVehicles()
        {
            List<Vehicle> parkedVehicles = new();
            foreach (var parkingSpot in _garage.ParkingSpots)
            {
                if (parkedVehicles.Contains(parkingSpot.OccupyingVechicle))
                {
                    continue;
                }
                else
                {
                    parkedVehicles.Add(parkingSpot.OccupyingVechicle);
                }
            }
            int parkedVechicleNumber = 1;
            foreach (var vehicle in parkedVehicles)
            {
                Console.WriteLine($"{parkedVechicleNumber}. {vehicle}");
                parkedVechicleNumber++;
            }
        }
    }
}