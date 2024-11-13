

namespace ParkingDeluxe;

    internal class ConsoleUI
    {
        private ParkingSpace _garage;

        public ConsoleUI(ParkingSpace garage)
        {
            _garage = garage;
        }

        internal void Start()
        {
            {
            ListParkingSpace();
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            bool runAgain = true;
            while (runAgain)
            {
                Console.WriteLine("1. Parkera ett fordon");
                Console.WriteLine("2. Checka ut ett fordon");
            Console.WriteLine("Q. Avsluta");

                ConsoleKeyInfo keyPress = Console.ReadKey(true);

                switch (keyPress.Key)
                {
                    case ConsoleKey.D1:
                        ParkMenu();
                        break;
                    case ConsoleKey.D2:
                        CheckoutMenu();
                        break;
                case ConsoleKey.Q:
                        runAgain = false;
                        break;
                } 
            }
        }

        private void CheckoutMenu()
        {
            bool runAgain = true;
            ListParkedVehicles();
        Console.WriteLine();

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
        ListParkingSpace();
        }

    private void ListParkedVehicles()
    {
        Console.WriteLine("Dessa fordon finns på parkeringen:");
        foreach (var vehicle in _garage.ParkedVehicles)
        {
            Console.WriteLine(vehicle);   
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


    internal void ListParkingSpace()
    {
        Console.SetCursorPosition(0, 1);
        HashSet<Vehicle> displayedVehicles = new();
        Console.WriteLine($"{" ",15} {"Registreringsnummer"} {"Fordonstyp"} {"Färg",-8} {"Övrigt",-8}");
        Console.WriteLine(new string('=',65));

        for (int i = 0; i < _garage.ParkingSpots.Count; i += 2)
        {
            var spot1 = _garage.ParkingSpots[i];
            var spot2 = i + 1 < _garage.ParkingSpots.Count ? _garage.ParkingSpots[i + 1] : null;

            if (spot1.OccupyingVechicle is not null && displayedVehicles.Contains(spot1.OccupyingVechicle))
            {
                continue;
            }

            // Handle vehicles that take multiple spots
            if (spot1.OccupyingVechicle is not null)
                {
                var vehicle = spot1.OccupyingVechicle;
                int vehicleSpan = vehicle.Size;
                int endSpot = (i + vehicleSpan - 1) / 2 + 1;

                if (vehicleSpan > 2)
                    {
                    Console.WriteLine($"{$"Plats {i / 2 + 1}-{endSpot}:",-15} {vehicle.Describe()}");
                    displayedVehicles.Add(vehicle);
                        continue;
                    }
            }

            // Handle motorcycle pairs
            if (spot1.OccupyingVechicle is MC && spot2?.OccupyingVechicle is MC)
                    {
                Console.WriteLine($"{$"Plats {i / 2 + 1}a:",-15} {spot1.OccupyingVechicle.Describe()}");
                Console.WriteLine($"{$"Plats {i / 2 + 1}b:",-15} {spot2.OccupyingVechicle.Describe()}");
                displayedVehicles.Add(spot1.OccupyingVechicle);
                displayedVehicles.Add(spot2.OccupyingVechicle);
                continue;
                    }

            // Handle regular vehicles
            string pairInfo = $"{$"Plats {i / 2 + 1}: ",-15} ";
            if (spot1.OccupyingVechicle is not null)
            {
                pairInfo += spot1.OccupyingVechicle.Describe();
                displayedVehicles.Add(spot1.OccupyingVechicle);
                }
            else if (spot2?.OccupyingVechicle is not null && !displayedVehicles.Contains(spot2.OccupyingVechicle))
            {
                pairInfo += spot2.OccupyingVechicle.Describe();
                displayedVehicles.Add(spot2.OccupyingVechicle);
            }
            else
            {
                pairInfo += "Tom";
            }
            Console.WriteLine(pairInfo);
        }
        Console.WriteLine();
    }


}