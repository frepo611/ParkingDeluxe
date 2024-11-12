
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
                        Console.ReadKey();
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
                //ShowMainMenu();
                //ListParkedVehicles();
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("1. Parkera ett fordon");
            Console.WriteLine("2. Checka ut ett fordon");
            Console.WriteLine("3. Avsluta");

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    ParkMenu();
                    break;
                case ConsoleKey.D2:
                    CheckoutMenu();
                    break;
                case ConsoleKey.D3:
                    break;
            }
        }

        private void CheckoutMenu()
        {
            ListParkedVehicles();
            bool regNumberIsValid = false;
            RegistrationNumber userEnteredRegNumber;
            while (true)
            {
                Console.Write("Ange registreringsnumret för ett parkerat fordon (3 siffror följt av 3 bokstäver utan mellanslag): ");
                string userEnteredString = Console.ReadLine();
                regNumberIsValid = RegistrationNumber.TryCreate(userEnteredString, out userEnteredRegNumber);
                if (_garage.Checkout(userEnteredRegNumber) is not null)
                {
                    Console.WriteLine($"Fordonet är utc");
                }
            }
        }

        private void ParkMenu()
        {
            throw new NotImplementedException();
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