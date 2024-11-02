
namespace ParkingDeluxe
{
    public class ParkingSpace
    {
        public List<HalfParkingSpot> ParkingSpots { get; }
        public int Count { get => ParkingSpots.Count; }

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

        public void Park(Vehicle vehicle)
        {
            if (vehicle is Car)
            {
                Car car = (Car)vehicle;
                Park(car);
            }
            else if (vehicle is MC)
            {
                MC mc = (MC)vehicle;
                Park(mc);
            }
            else if (vehicle is Bus)
            {
                Bus bus = (Bus)vehicle;
                Park(bus);
            }
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
                ParkingSpots[parkingSpotIndex].OccupiedBy = car;
                ParkingSpots[parkingSpotIndex + 1].OccupiedBy = car;
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
                ParkingSpots[parkingSpotIndex].OccupiedBy = mc;
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
                ParkingSpots[parkingSpotIndex].OccupiedBy = bus;
                ParkingSpots[parkingSpotIndex].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 1].OccupiedBy = bus;
                ParkingSpots[parkingSpotIndex + 1].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 2].OccupiedBy = bus;
                ParkingSpots[parkingSpotIndex + 2].IsEmpty = false;
                ParkingSpots[parkingSpotIndex + 3].OccupiedBy = bus;
                ParkingSpots[parkingSpotIndex + 3].IsEmpty = false;
            }
            return parkingSuccess;
        }
        public bool Checkout(Car car)
        {
            bool checkoutSuccess = false;
            for (int i = 0; i < Count; i++)
            {
                if (ParkingSpots[i].OccupiedBy == car)
                {
                    ParkingSpots[i].OccupiedBy = null;
                    ParkingSpots[i + 1].OccupiedBy = null;
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
                if (ParkingSpots[i].OccupiedBy == mc)
                {
                    ParkingSpots[i].OccupiedBy = null;
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
            for (int i = 0; i < Count - 4; i += 4 )
            {
                if (ParkingSpots[i].OccupiedBy == bus)
                {
                    ParkingSpots[i].OccupiedBy = null;
                    ParkingSpots[i].IsEmpty = true;
                    ParkingSpots[i + 1].OccupiedBy = null;
                    ParkingSpots[i + 1].IsEmpty = true;
                    ParkingSpots[i + 2].OccupiedBy = null;
                    ParkingSpots[i + 2].IsEmpty = true;
                    ParkingSpots[i + 3].OccupiedBy = null;
                    ParkingSpots[i + 3].IsEmpty = true;
                    checkoutSuccess = true;
                    break;
                }
            }
            return checkoutSuccess;
        }
    }


}