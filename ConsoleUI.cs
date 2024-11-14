

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
            Console.Write("Ange registreringsnumret för ett parkerat fordon (3 siffror följt av 3 bokstäver utan mellanslag). Tomt registreringsnummer för att gå till den tidigare menyn: ");
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
        bool runAgain = true;
        while (runAgain)
        {
            Console.WriteLine("Vad för typ av fordon vill du parkera?");
            Console.WriteLine("1. MC");
            Console.WriteLine("2. Bil");
            Console.WriteLine("3. Buss");
            Console.WriteLine("Q. Gå till den tidigare menyn");

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    MCMenu();
                    break;
                case ConsoleKey.D2:
                    BilMenu();
                    break;
                case ConsoleKey.D3:
                    runAgain = false;
                    BusMenu();
                    break;
                case ConsoleKey.Q:
                    runAgain = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void BusMenu()
    {
        Console.WriteLine("Checka in en buss");
        RegistrationNumber registrationNumber = RegistrationNumber.GetRandom();
        Console.WriteLine($"Registreringsnummer: {registrationNumber.RegNumber}");
        string vehicleColor = GetColorFromUser();

        int passengers = 0;
        bool passengersIsValid = false;
        while (!passengersIsValid)
        {
            Console.Write("Hur många passagerare: ");
            passengersIsValid = int.TryParse(Console.ReadLine(), out passengers);
        }
        Bus busToPark = new Bus(registrationNumber, vehicleColor, passengers);
        if (_garage.Park(busToPark))
        {
            Console.WriteLine($"{busToPark} är parkerad");
        }
        else
        {
            Console.WriteLine("Fordonet kunde inte parkeras.");
        }

    }
    private string GetColorFromUser()
    {
        bool validColor = false;
        string vehicleColor = Colors.AvailableColors[0];
        Console.WriteLine("Välj fordonets färg:");
        while (!validColor)
        {
            for (int i = 0; i < Colors.AvailableColors.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Colors.AvailableColors[i]}");
            }

            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            vehicleColor = keyPress.Key switch
            {
                ConsoleKey.D1 => Colors.AvailableColors[0],
                ConsoleKey.D2 => Colors.AvailableColors[1],
                ConsoleKey.D3 => Colors.AvailableColors[2],
                ConsoleKey.D4 => Colors.AvailableColors[3],
                ConsoleKey.D5 => Colors.AvailableColors[4],
                ConsoleKey.D6 => Colors.AvailableColors[5],
                ConsoleKey.D7 => Colors.AvailableColors[6],
                _ => String.Empty
            };

            validColor = !string.IsNullOrEmpty(vehicleColor);
            if (!validColor)
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }

        return vehicleColor;
    }


    private void BilMenu()
    {
        Console.WriteLine("Parkera en bil");
        RegistrationNumber registrationNumber = RegistrationNumber.GetRandom();
        Console.WriteLine($"Registreringsnummer: {registrationNumber.RegNumber}");
        string vehicleColor = GetColorFromUser();

        bool isElectric = false;
        bool validChoice = false;
        while (!validChoice)
        {
            Console.WriteLine("Är det en elbil (J/N)? ");
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.J:
                    isElectric = true;
                    validChoice = true;
                    break;
                case ConsoleKey.N:
                    isElectric = false;
                    validChoice = true;
                    break;
                default:
                    break;
            }
        }
        Car carToPark = new Car(registrationNumber, vehicleColor, isElectric);
        if (_garage.Park(carToPark))
        {
            Console.WriteLine($"{carToPark} är parkerad.");
        }
        else
        {
            Console.WriteLine("Bilen kunde inte parkeras.");
        }
    }

    internal void MCMenu()
    {
        RegistrationNumber registrationNumber = RegistrationNumber.GetRandom();
        Console.WriteLine($"Registreringsnummer: {registrationNumber.RegNumber}");

        string vehicleColor = GetColorFromUser();
        string mcBrand = GetMCBrandFromUser();

        MC mcToPark = new MC(registrationNumber, vehicleColor, mcBrand);
        if (_garage.Park(mcToPark))
        {
            Console.WriteLine($"{mcToPark} är parkerad.");
        }
        else
        {
            Console.WriteLine("MC:n kunde inte parkeras");
        }

    }
    private static string GetMCBrandFromUser()
    {
        bool validBrand = false;
        string mcBrand = McBrands.AvailableBrands[0];

        while (!validBrand)
        {
            Console.WriteLine("Välj motorcykelns märke:");
            for (int i = 0; i < McBrands.AvailableBrands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {McBrands.AvailableBrands[i]}");
            }

            ConsoleKeyInfo keyPress = Console.ReadKey(true);

            mcBrand = keyPress.Key switch
            {
                ConsoleKey.D1 => McBrands.AvailableBrands[0],
                ConsoleKey.D2 => McBrands.AvailableBrands[1],
                ConsoleKey.D3 => McBrands.AvailableBrands[2],
                ConsoleKey.D4 => McBrands.AvailableBrands[3],
                ConsoleKey.D5 => McBrands.AvailableBrands[4],
                _ => string.Empty
            };

            validBrand = !string.IsNullOrEmpty(mcBrand);
            if (!validBrand)
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }

        return mcBrand;
    }


    internal void ListParkingSpace()
    {
        Console.SetCursorPosition(0, 1);
        HashSet<Vehicle> displayedVehicles = new();
        Console.WriteLine($"{" ",15} {"Registreringsnummer"} {"Fordonstyp"} {"Färg",-8} {"Övrigt",-8}");
        Console.WriteLine(new string('=', 65));

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

    internal void StartSim()
    {
        ListParkingSpace();
        for (int i = 0; i < 5; i++)
        {
            _garage.Park(Utilities.GetRandomVehicle());
        }
        ListParkingSpace();
        while (true)
        {
            if (Random.Shared.Next(0, 2) > 0)
            {
                _garage.Park(Utilities.GetRandomVehicle());
                ListParkingSpace();
                Thread.Sleep(1000);
            }
            else
            {
                _garage.Checkout(Utilities.CheckoutRandomVehicle(_garage));
                ListParkingSpace();
                Thread.Sleep(1000);
            }

}