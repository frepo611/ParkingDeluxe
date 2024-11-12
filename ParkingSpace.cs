
namespace ParkingDeluxe
{
    public class ParkingSpace
    {
        public List<HalfParkingSpot> ParkingSpots { get; }
        public int Count { get => ParkingSpots.Count; }
        public ParkingSpace(int size)
        {
            ParkingSpots = new List<HalfParkingSpot>(size * 2);
            for (int i = 0; i < (size * 2); i++)
            {
                ParkingSpots.Add(new HalfParkingSpot(i));
            }
        }

        public bool Park(Vehicle vehicle)
        {
            if (!ParkFirstPerfectFit(vehicle))
            {
                return ParkFirstFit(vehicle);
            }
            return true;
        }

        private bool ParkFirstPerfectFit(Vehicle vehicle)
        {
            for (int i = 0; i < Count - vehicle.Size; i += (vehicle.Size == 1) ? 1 : 2)
            {
                bool perfectFit = true;
                // check if the entire vehicle can fit
                for (global::System.Int32 j = 0; j < vehicle.Size; j++)
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

        public ParkingSpace(int size)
        {
            ParkingSpots = new List<HalfParkingSpot>(size * 2);
            for (int i = 0; i < (size * 2); i++)
            {
                ParkingSpots.Add(new HalfParkingSpot(i));
            }
        }

        public bool Park(Vehicle vehicle)
        {
            bool parkingSuccess;
            if (vehicle is Car)
            {
                Car car = (Car)vehicle;
                parkingSuccess = Park(car);
            }
            else if (vehicle is MC)
            {
                MC mc = (MC)vehicle;
                return Park(mc);
            }
            else if (vehicle is Bus)
            {
                Bus bus = (Bus)vehicle;
                return Park(bus);
            }
            else throw new ArgumentException("Cannot park this vehicle.");
            vehicle.StartTimedAction();
            return parkingSuccess;
        }
        public bool Checkout(Vehicle vehicle)
        {
            bool checkoutSuccess = false;
            if (vehicle is null)
                return checkoutSuccess;
            if (vehicle is Car)
            {
                Car car = (Car)vehicle;
                checkoutSuccess = Checkout(car);
            }
            else if (vehicle is MC)
            {
                MC mc = (MC)vehicle;
                checkoutSuccess = Checkout(mc);
            }
            else if (vehicle is Bus)
            {
                Bus bus = (Bus)vehicle;
                checkoutSuccess = Checkout(bus);
            }
            vehicle.EndTimedAction();
            return checkoutSuccess;
        }

        private bool Park(Car car)
        {
            int parkingSpotIndex = GetStartOfLargestEmptySpace();
            bool parkingSuccess = false;

            // park in the largest empty space
            if (ParkingSpots[parkingSpotIndex].IsEmpty && ParkingSpots[parkingSpotIndex + 1].IsEmpty)
            {
                parkingSuccess = true;
            }
            if (parkingSuccess)
            {
                ParkingSpots[parkingSpotIndex].OccupyingVechicle = car;
                ParkingSpots[parkingSpotIndex + 1].OccupyingVechicle = car;
                ParkingSpots[parkingSpotIndex].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 1].IsEmpty = false;
            }
            return parkingSuccess;
        }
        private bool Park(MC mc)
        {
            int parkingSpotIndex = 0;
            bool parkingSuccess = false;
            // check if there is already a parked MC in an even spot with a free odd spot
            for (parkingSpotIndex = 1; parkingSpotIndex < Count; parkingSpotIndex += 2)
            {
                if (!ParkingSpots[parkingSpotIndex - 1].IsEmpty && ParkingSpots[parkingSpotIndex].IsEmpty)
                {
                    parkingSuccess = true;
                    break;
                }
            }
            // check all even spots
            if (!parkingSuccess)
            {
                parkingSpotIndex = GetStartOfLargestEmptySpace();
                if (ParkingSpots[parkingSpotIndex].IsEmpty)
                {
                    parkingSuccess = true;
                }
            }
            if (parkingSuccess)
            {
                ParkingSpots[parkingSpotIndex].OccupyingVechicle = mc;
                ParkingSpots[parkingSpotIndex].IsEmpty = false;
            }
            return parkingSuccess;
        }
        private bool Park(Bus bus)
        {
            bool parkingSuccess = false;
            int parkingSpotIndex = GetStartOfLargestEmptySpace();
            if (ParkingSpots[parkingSpotIndex].IsEmpty
                && ParkingSpots[parkingSpotIndex + 1].IsEmpty
                && ParkingSpots[parkingSpotIndex + 2].IsEmpty
                && ParkingSpots[parkingSpotIndex + 3].IsEmpty)
            {
                parkingSuccess = true;
            }
            if (parkingSuccess)
            {
                ParkingSpots[parkingSpotIndex].OccupyingVechicle = bus;
                ParkingSpots[parkingSpotIndex].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 1].OccupyingVechicle = bus;
                ParkingSpots[parkingSpotIndex + 1].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 2].OccupyingVechicle = bus;
                ParkingSpots[parkingSpotIndex + 2].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 3].OccupyingVechicle = bus;
                ParkingSpots[parkingSpotIndex + 3].IsEmpty = false;
            }
            return parkingSuccess;
        }
        public bool Checkout(Car car)
        {
            bool checkoutSuccess = false;
            for (int i = 0; i < Count; i++)
            {
                if (ParkingSpots[i].OccupyingVechicle == car)
                {
                    ParkingSpots[i].OccupyingVechicle = null;
                    ParkingSpots[i + 1].OccupyingVechicle = null;
                    ParkingSpots[i].IsEmpty = true;
                    ParkingSpots[i + 1].IsEmpty = true;
                    checkoutSuccess = true;
                    break;
                }
            }
            return checkoutSuccess;
        }
        private bool Checkout(MC mc)
        {
            bool checkoutSuccess = false;
            for (int i = 0; i < Count; i++)
            {
                if (ParkingSpots[i].OccupyingVechicle == mc)
                {
                    ParkingSpots[i].OccupyingVechicle = null;
                    ParkingSpots[i].IsEmpty = true;
                    checkoutSuccess = true;
                    break;
                }
            }
            return checkoutSuccess;
        }
        private bool Checkout(Bus bus)
        {
            bool checkoutSuccess = false;
            for (int i = 0; i < Count - 4; i += 4)
            {
                if (ParkingSpots[i].OccupyingVechicle == bus)
                {
                    ParkingSpots[i].OccupyingVechicle = null;
                    ParkingSpots[i].IsEmpty = true;
                    ParkingSpots[i + 1].OccupyingVechicle = null;
                    ParkingSpots[i + 1].IsEmpty = true;
                    ParkingSpots[i + 2].OccupyingVechicle = null;
                    ParkingSpots[i + 2].IsEmpty = true;
                    ParkingSpots[i + 3].OccupyingVechicle = null;
                    ParkingSpots[i + 3].IsEmpty = true;
                    checkoutSuccess = true;
                    break;
                }
            }
            return checkoutSuccess;
        }
    }


}