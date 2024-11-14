

namespace ParkingDeluxe;

public class ParkingSpace
{
    private const double CostPerMinute = 1.5;

    public List<HalfParkingSpot> ParkingSpots { get; }
    public int Count { get => ParkingSpots.Count; }
    public Dictionary<RegistrationNumber, Vehicle> ParkedVehicles { get; }
    public ParkingSpace(int size)
    {
        ParkingSpots = new List<HalfParkingSpot>(size * 2);
        for (int i = 0; i < (size * 2); i++)
        {
            ParkingSpots.Add(new HalfParkingSpot(i));
        }
        ParkedVehicles = new Dictionary<RegistrationNumber, Vehicle>();
    }

    public bool Park(Vehicle vehicle)
    {
        if (ParkFirstPerfectFit(vehicle))
        {
            vehicle.StartTimer();
            ParkedVehicles.TryAdd(vehicle.RegistrationNumber, vehicle);
            return true;
        }
        else if (ParkFirstFit(vehicle))
        {
            vehicle.StartTimer();
            ParkedVehicles.TryAdd(vehicle.RegistrationNumber, vehicle);
            return true;
        }
        return false;
    }

    private bool ParkFirstPerfectFit(Vehicle vehicle)
    {
        bool isPerfectFit = false;

        if (vehicle.Size == 1) // Special case
        {
            int i = 0;
            for (i = 0; i < ParkingSpots.Count; i++)
            {
                if (i == 0
                    && ParkingSpots[i].IsEmpty
                    && !ParkingSpots[i + 1].IsEmpty) // first spot is perfect
                {
                    //ParkingSpots[i].IsEmpty = false;
                    //ParkingSpots[i].OccupyingVechicle = vehicle;
                    isPerfectFit = true;
                    break;
                }
                //else if (i == 0 && ParkingSpots[i + 1].IsEmpty)
                //{
                //    //ParkingSpots[i].IsEmpty = false;
                //    //ParkingSpots[i].OccupyingVechicle = vehicle;
                //    perfectFit = true;
                //    break;
                //}
                else if ((i == ParkingSpots.Count - 1) && !ParkingSpots[i - 1].IsEmpty) //second to last spot is perfect
                {
                    //ParkingSpots[i].IsEmpty = false;
                    //ParkingSpots[i].OccupyingVechicle = vehicle;
                    isPerfectFit = true;
                    break;
                }
                else if ((i == ParkingSpots.Count) && ParkingSpots[i - 1].IsEmpty) //last spot is perfect
                {
                    //ParkingSpots[i].IsEmpty = false;
                    //ParkingSpots[i].OccupyingVechicle = vehicle;
                    isPerfectFit = true;
                    break;
                }
                else if (i > 0 && (i < ParkingSpots.Count - 1))
                {
                    if (ParkingSpots[i].IsEmpty
                        && !ParkingSpots[i - 1].IsEmpty)
                    {
                        ParkingSpots[i].IsEmpty = false;
                        ParkingSpots[i].OccupyingVechicle = vehicle;
                        isPerfectFit = true;
                        break;
                    }
                }
            }
            if (isPerfectFit)
            {
                ParkingSpots[i].IsEmpty = false;
                ParkingSpots[i].OccupyingVechicle = vehicle;
                return isPerfectFit;
            }
            return isPerfectFit;
        }

        for (int i = 0; i < Count - vehicle.Size; i += 2)
        {
            // Check if current spot is empty
            if (!ParkingSpots[i].IsEmpty)
                continue;

            // Check if all needed spots are empty
            bool spotsAreEmpty = true;
            for (int j = 0; j < vehicle.Size; j++)
            {
                if (!ParkingSpots[i + j].IsEmpty)
                {
                    spotsAreEmpty = false;
                    break;
                }
            }

            if (!spotsAreEmpty)
                continue;

            // Check for perfect fit - occupied spots on both sides
            isPerfectFit = (i == 0 || !ParkingSpots[i - 1].IsEmpty) &&
                                (i + vehicle.Size >= Count || !ParkingSpots[i + vehicle.Size].IsEmpty);

            if (isPerfectFit)
            {
                for (int j = 0; j < vehicle.Size; j++)
                {
                    ParkingSpots[i + j].IsEmpty = false;
                    ParkingSpots[i + j].OccupyingVechicle = vehicle;
                }
                return isPerfectFit;
            }
        }
        return isPerfectFit;
    }


    private bool ParkFirstFit(Vehicle vehicle)
    {
        for (int i = 0; i < Count - vehicle.Size; i += (vehicle.Size == 1) ? 1 : 2)
        {
            bool canFit = true;
            // check if the entire vehicle can fit
            for (int j = 0; j < vehicle.Size; j++)
            {
                if (!ParkingSpots[i + j].IsEmpty)
                {
                    canFit = false;
                    break;
                }
            }
            if (canFit)
            {
                for (int j = 0; j < vehicle.Size; j++)
                {
                    ParkingSpots[i + j].IsEmpty = false;
                    ParkingSpots[i + j].OccupyingVechicle = vehicle;
                }
                return true;
            }
        }
        return false;
    }

    private int GetStartOfLargestEmptySpace()
    {

        int startIndex = 0;
        int largestSpace = 0;
        int currentSpace = 0;
        int currentIndex = 0;
        for (int i = 0; i < Count - 2; i += 2)
        {
            if (ParkingSpots[i].IsEmpty && ParkingSpots[i + 1].IsEmpty)
            {
                if (currentSpace == 0) // start a new contiguous space
                {
                    currentIndex = i;
                }
                currentSpace++;
                if (currentSpace > largestSpace)
                {
                    largestSpace = currentSpace;
                    startIndex = currentIndex;
                }
            }
            else
            {
                currentSpace = 0;
            }
        }
        return startIndex;
    }
    public Vehicle? Checkout(Vehicle vehicle)
    {
        if (vehicle is null)
        {
            return null;
        }
        Vehicle? checkedOutVehicle = null;
        for (int i = 0; i < Count - vehicle.Size; i++)
        {
            if (ParkingSpots[i].OccupyingVechicle == vehicle)
            {
                checkedOutVehicle = ParkingSpots[i].OccupyingVechicle;
                for (int j = 0; j < vehicle.Size; j++)
                {
                    ParkingSpots[i + j].OccupyingVechicle = null;
                    ParkingSpots[i + j].IsEmpty = true;
                }
            }
        }
        if (checkedOutVehicle is not null)
        {
            vehicle.EndTimer();
            ParkedVehicles.Remove(checkedOutVehicle.RegistrationNumber);
            return checkedOutVehicle;
        }
        return null;
    }
    public Vehicle? Checkout(RegistrationNumber regNumber)
    {
        Vehicle? foundVehicle = null;
        foreach (var spot in ParkingSpots.Where(spot => spot.OccupyingVechicle is not null))
        {
            if (regNumber == spot.OccupyingVechicle.RegistrationNumber)
            {
                foundVehicle = spot.OccupyingVechicle;
                break;
            }
        }
        if (foundVehicle is not null)
            return Checkout(foundVehicle);
        else return null;
    }

    public double ParkingFee(Vehicle checkedOutVehicle)
    {
        return checkedOutVehicle.GetElapsedTime().Seconds * CostPerMinute;
    }
}

