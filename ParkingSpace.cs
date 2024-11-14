

namespace ParkingDeluxe
{
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
            if (vehicle.Size == 1)
            {
                int i = 0;
                bool perfectFit = false;
                for (i = 0; i < ParkingSpots.Count; i++)
                {
                    if (i == 0 && !ParkingSpots[i + 1].IsEmpty)
                    {
                        ParkingSpots[i].IsEmpty = false;
                        ParkingSpots[i].OccupyingVechicle = vehicle;
                        return perfectFit;
                    }
                    else if ((i == ParkingSpots.Count - 1) && !ParkingSpots[i - 1].IsEmpty)
                    {
                        ParkingSpots[i].IsEmpty = false;
                        ParkingSpots[i].OccupyingVechicle = vehicle;
                        return perfectFit;
                    }
                    else if (i > 1 && (i < ParkingSpots.Count - 1))
                    {
                        if (ParkingSpots[i].IsEmpty
                            && !ParkingSpots[i - 1].IsEmpty
                            && !ParkingSpots[i + 1].IsEmpty)
                        {
                            ParkingSpots[i].IsEmpty = false;
                            ParkingSpots[i].OccupyingVechicle = vehicle;
                            return perfectFit;
                        }
                    }
                }
                if (perfectFit)
                {
                    ParkingSpots[i].IsEmpty = false;
                    ParkingSpots[i].OccupyingVechicle = vehicle;
                    return perfectFit;
                }
            }

            for (int i = 0; i < Count - vehicle.Size; i += (vehicle.Size == 1) ? 1 : 2)
            {
                bool perfectFit = true;
                // check if the entire vehicle can fit
                for (int j = 0; j < vehicle.Size; j++)
                {
                    if (!ParkingSpots[i + j].IsEmpty)
                    {
                        perfectFit = false;
                        break;
                    }
                    if (!ParkingSpots[i + vehicle.Size].IsEmpty)
                    {
                        perfectFit = false;
                        break;
                    }
                }
                if (perfectFit)
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

        private bool ParkFirstFit(Vehicle vehicle)
        {
            for (int i = 0; i < Count - vehicle.Size; i += (vehicle.Size == 1) ? 1 : 2)
            {
                bool canFit = true;
                // check if the entire vehicle can fit
                for (global::System.Int32 j = 0; j < vehicle.Size; j++)
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


}