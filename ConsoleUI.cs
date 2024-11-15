﻿namespace ParkingDeluxe;

internal class ConsoleUI
{
    private readonly ParkingSpace _garage;
    private const int _minLineLength = 40;

    public ConsoleUI(ParkingSpace garage)
    {
        _garage = garage;
    }

    internal void Start()
    {
        {
            Console.CursorVisible = true;
            ShowMainMenu();
        }
    }

    private void ShowMainMenu()
    {
        bool runAgain = true;
        while (runAgain)
        {
            Console.Clear();
            ListParkingSpace();
            WriteLineWithPadding("Huvudmeny");
            WriteLineWithPadding("1. Parkera ett fordon");
            WriteLineWithPadding("2. Checka ut ett fordon");
            WriteLineWithPadding("Q. Avsluta");

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
        if (_garage.ParkedVehicles.Count == 0)
        {
            WriteLineWithPadding("Det finns inga parkerade fordon.");
            Console.WriteLine("Tryck en tangent för att fortsätta");
            Console.ReadKey();
            return;
        }

        bool runAgain = true;
        Console.Clear();
        ListParkingSpace();
        ListParkedVehicles();

        while (runAgain)
        {
            Console.Write("Ange registreringsnumret för ett parkerat fordon (3 bokstäver följt 3 siffror av utan mellanslag).\nTomt registreringsnummer för att gå till den tidigare menyn: ");
            string? userEnteredString = Console.ReadLine();
            if (string.IsNullOrEmpty(userEnteredString))
            {
                runAgain = false;
                continue;
            }
            Vehicle? checkedOutVehicle = null;
            RegistrationNumber.TryCreate(userEnteredString, out RegistrationNumber? userEnteredRegNumber);
            if (userEnteredRegNumber is not null)
            { 
                checkedOutVehicle = _garage.Checkout(userEnteredRegNumber);
            }

            if (checkedOutVehicle is not null)
            {
                Console.Clear();
                ListParkingSpace();
                WriteLineWithPadding($"{checkedOutVehicle} är utcheckad. Parkeringskostnaden är {(checkedOutVehicle.GetElapsedMinutes() * _garage.CostPerMinute):C}");
            }
        }
    }

    private void ListParkedVehicles()
    {
        WriteLineWithPadding("Dessa fordon finns på parkeringen:");
        foreach (var vehicle in _garage.ParkedVehicles.Values)
        {
            WriteLineWithPadding(vehicle.ToString());
        }
    }

    private void ParkMenu()
    {
        bool runAgain = true;
        while (runAgain)
        {
            Console.Clear();
            ListParkingSpace();
            Console.WriteLine("Parkeringsmeny");
            WriteLineWithPadding("Vad för typ av fordon vill du parkera?");
            WriteLineWithPadding("1. MC");
            WriteLineWithPadding("2. Bil");
            WriteLineWithPadding("3. Buss");
            WriteLineWithPadding("Q. Gå till den tidigare menyn");

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    ParkMCMenu();
                    break;
                case ConsoleKey.D2:
                    ParkCarMenu();
                    break;
                case ConsoleKey.D3:
                    ParkBusMenu();
                    break;
                case ConsoleKey.Q:
                    runAgain = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void ParkBusMenu()
    {
        Console.Clear();
        ListParkingSpace();
        WriteLineWithPadding("Parkera en buss");

        RegistrationNumber? registrationNumber = GetRegistrationNumber();
        if (registrationNumber is null)
        {
            return;
        }

        WriteLineWithPadding($"Registreringsnummer: {registrationNumber.RegNumber}");
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
            WriteLineWithPadding($"{busToPark} parkeras.");
        }
        else
        {
            WriteLineWithPadding("Bussen kunde inte parkeras.");
        }
        Console.WriteLine("Tryck en tangent för att fortsätta");
        Console.ReadKey();
        Console.Clear();
        ListParkingSpace();
    }

    private static RegistrationNumber? GetRegistrationNumber()
    {
        bool runAgain = true;
        RegistrationNumber? registrationNumber = null;
        while (runAgain)
        {
            Console.WriteLine("1. Ange registreringsnummer");
            Console.WriteLine("2. Generera registreringsnummer");
            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:
                    registrationNumber = GetRegistrationFromUser();
                    runAgain = false;
                    break;
                case ConsoleKey.D2:
                    runAgain = false;
                    registrationNumber = RegistrationNumber.GetRandom();
                    break;
                default:
                    break;
            }
        }
        return registrationNumber;
    }

    private static string GetColorFromUser()
    {
        bool validColor = false;
        string vehicleColor = Colors.AvailableColors[0];
        WriteLineWithPadding("Välj fordonets färg:");

        while (!validColor)
        {
            for (int i = 0; i < Colors.AvailableColors.Length; i++)
            {
                WriteLineWithPadding($"{i + 1}. {Colors.AvailableColors[i]}");
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
                WriteLineWithPadding("Ogiltigt val. Försök igen.");
            }
        }

        return vehicleColor;
    }
    private static RegistrationNumber? GetRegistrationFromUser()
    {
        bool runAgain = true;
        RegistrationNumber? userEnteredRegNumber = null;

        while (runAgain)
        {
            bool regNumberIsValid;
            Console.Write("Ange registreringsnumret (3 bokstäver följt av 3 siffror utan mellanslag).\nTomt registreringsnummer för att gå till den tidigare menyn: ");
            string? userEnteredString = Console.ReadLine();

            if (string.IsNullOrEmpty(userEnteredString))
            {
                break;
            }

            regNumberIsValid = RegistrationNumber.TryCreate(userEnteredString, out userEnteredRegNumber);

            if (regNumberIsValid)
            {
                runAgain = false;
            }
        }
        return userEnteredRegNumber;
    }
    private void ParkCarMenu()
    {
        Console.Clear();
        ListParkingSpace();
        WriteLineWithPadding("Parkera en bil");
        RegistrationNumber? registrationNumber = GetRegistrationNumber();
        if (registrationNumber is null)
        {
            return;
        }
        WriteLineWithPadding($"Registreringsnummer: {registrationNumber.RegNumber}");

        string vehicleColor = GetColorFromUser();
        bool isElectric = false;
        bool validChoice = false;

        while (!validChoice)
        {
            WriteLineWithPadding($"{"Är det en elbil (J/N)? ",-40}");
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
            WriteLineWithPadding($"{carToPark} parkeras.");
        }
        else
        {
            WriteLineWithPadding("Bilen kunde inte parkeras.");
        }
        Console.WriteLine("Tryck en tangent för att fortsätta");
        Console.ReadKey();
        Console.Clear();
        ListParkingSpace();
    }

    internal void ParkMCMenu()
    {
        Console.Clear();
        ListParkingSpace();
        Console.WriteLine("Parkera en MC");
        RegistrationNumber? registrationNumber = GetRegistrationNumber();

        if (registrationNumber is null)
        {
            return;
        }
        WriteLineWithPadding($"Registreringsnummer: {registrationNumber.RegNumber}");

        string vehicleColor = GetColorFromUser();
        string mcBrand = GetMCBrandFromUser();

        MC mcToPark = new MC(registrationNumber, vehicleColor, mcBrand);

        if (_garage.Park(mcToPark))
        {
            WriteLineWithPadding($"{mcToPark} parkeras.");
        }
        else
        {
            WriteLineWithPadding("MC:n kunde inte parkeras");
        }
        Console.WriteLine("Tryck en tangent för att fortsätta");
        Console.ReadKey();
        Console.Clear();
        ListParkingSpace();
    }

    private static string GetMCBrandFromUser()
    {
        bool validBrand = false;
        string mcBrand = McBrands.AvailableBrands[0];

        while (!validBrand)
        {
            WriteLineWithPadding("Välj motorcykelns märke:");
            for (int i = 0; i < McBrands.AvailableBrands.Length; i++)
            {
                WriteLineWithPadding($"{i + 1}. {McBrands.AvailableBrands[i]}");
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
        Console.WriteLine(new string('=', 72));

        for (int i = 0; i < _garage.ParkingSpots.Count; i += 2)
        {
            HalfParkingSpot spot1 = _garage.ParkingSpots[i];
            HalfParkingSpot? spot2 = i + 1 < _garage.ParkingSpots.Count ? _garage.ParkingSpots[i + 1] : null;

            if (spot1.OccupyingVechicle is not null && displayedVehicles.Contains(spot1.OccupyingVechicle))
            {
                continue;
            }

            // Handle vehicles that take multiple spots
            if (spot1.OccupyingVechicle is not null)
            {
                Vehicle vehicle = spot1.OccupyingVechicle;
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
                pairInfo += $"Tom".PadRight(100);
            }
            Console.WriteLine(pairInfo);
        }
        Console.WriteLine();
    }

    private static void WriteLineWithPadding(string text)
    {
        Console.WriteLine(text.PadRight(_minLineLength));
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
            Console.SetCursorPosition(0, 20);
            if (Random.Shared.Next(0, 3) < 2)
            {
                var vehicleToPark = Utilities.GetRandomVehicle();
                Console.WriteLine($"{vehicleToPark} is getting parked {new string(' ', 20)}");
                _garage.Park(vehicleToPark);
                Console.WriteLine($"{"Paused...",-40}");
                //Console.ReadKey();
                ListParkingSpace();
            }
            else
            {
                if (_garage.ParkedVehicles.Count > 0)
                {
                    Vehicle vehicleToCheckOut = Utilities.CheckoutRandomVehicle(_garage);
                    _garage.Checkout(vehicleToCheckOut);
                    Console.WriteLine($"{"Paused...",-40}");
                    Console.WriteLine($"{vehicleToCheckOut} was checked out {vehicleToCheckOut.GetElapsedMinutes() * _garage.CostPerMinute:C}");
                    //Console.ReadKey();
                    ListParkingSpace();
                }
            }
            Thread.Sleep(5000);
            Console.SetCursorPosition(0, 20);
        }
    }
}