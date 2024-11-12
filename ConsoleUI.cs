
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
                        Vehicle? vehicle = Utilities.CheckoutRandomVehicle(_garage);
                        Console.WriteLine($"{vehicle} checked out. Parking time: {vehicle.GetElapsedTime().Seconds} seconds.");
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
                        Vehicle? occupyingVechicle = _garage.ParkingSpots[i].OccupyingVechicle;
                        Console.WriteLine($"{_garage.ParkingSpots[i].ID}: {(occupyingVechicle == null ? String.Empty : occupyingVechicle.Describe())}");
                    }
                }
                Console.ReadKey();
                ShowMainMenu();
                ListParkedVehicles();
            }
        }

        private void ShowMainMenu()
        {
            bool runAgain = true;
            while (runAgain)
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
                        runAgain = false;
                        break;
                } 
            }
        }

        private void CheckoutMenu()
        {
            bool runAgain = true;
            ListParkedVehicles();
            while (runAgain)
            {
                bool regNumberIsValid = false;
                RegistrationNumber userEnteredRegNumber;
                Console.Write("Ange registreringsnumret för ett parkerat fordon (3 siffror följt av 3 bokstäver utan mellanslag). Tomt registreringsnummer för att gå tillbaka: ");
                string? userEnteredString = Console.ReadLine();
                if (string.IsNullOrEmpty(userEnteredString))
                {
                    runAgain = false;
                    continue;
                }
                regNumberIsValid = RegistrationNumber.TryCreate(userEnteredString, out userEnteredRegNumber);
                Vehicle? checkedOutVehicle = _garage.Checkout(userEnteredRegNumber);
                if (checkedOutVehicle is not null)
                {
                    Console.WriteLine($"{checkedOutVehicle} är utcheckad. Parkeringskostnaden är {_garage.ParkingFee(checkedOutVehicle):C}");
                }
            }
        }

        private void ParkMenu()
        {
            return;
        }

        internal void ListParkedVehicles()
        {
            if (_garage.Count == 0)
            {
                Console.WriteLine("Parkeringen är tom");
                return;
            }
            List<Vehicle> parkedVehicles = new();
            foreach (var parkingSpot in _garage.ParkingSpots)
            {

                if (parkingSpot.OccupyingVechicle is not null)
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