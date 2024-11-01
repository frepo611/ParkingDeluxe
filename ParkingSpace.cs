
namespace ParkingDeluxe
{
    public class ParkingSpace
    {
        public List<ParkingSpot> ParkingSpots;
        public int Count { get => ParkingSpots.Count; }
        public ParkingSpace(int size)
        {
            ParkingSpots = new List<ParkingSpot>(size * 2);
            for (int i = 0; i < (size * 2); i++)
            {
                ParkingSpots.Add(new ParkingSpot(i));
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
            else
            {
                throw new NotImplementedException();
            }
        }

        private void Park(Car car)
        {
            int parkingSpotIndex = 0;
            for (parkingSpotIndex = 0; parkingSpotIndex < ParkingSpots.Count; parkingSpotIndex += 2)
            {
                if (ParkingSpots[parkingSpotIndex].IsEmpty && ParkingSpots[parkingSpotIndex + 1].IsEmpty)
                    break;
            }
            ParkingSpots[parkingSpotIndex].OccupiedBy = car;
            ParkingSpots[parkingSpotIndex + 1].OccupiedBy = car;
            ParkingSpots[parkingSpotIndex].IsEmpty = false;
            ParkingSpots[parkingSpotIndex + 1].IsEmpty = false;
        }
        private void Park(MC mc)
        {
            int parkingSpotIndex = 0;
            for (parkingSpotIndex = 1; parkingSpotIndex < ParkingSpots.Count; parkingSpotIndex += 2)
            {
                if (ParkingSpots[parkingSpotIndex].IsEmpty && !ParkingSpots[parkingSpotIndex - 1].IsEmpty)
                    break;
            }
            for (parkingSpotIndex = 0; parkingSpotIndex < ParkingSpots.Count; parkingSpotIndex += 2)
            {
                if (ParkingSpots[parkingSpotIndex].IsEmpty)
                    break;
            }

            ParkingSpots[parkingSpotIndex].OccupiedBy = mc;
            ParkingSpots[parkingSpotIndex].IsEmpty = false;
        }

        public void Checkout(Car car)
        {
            for (int i = 0; i < ParkingSpots.Count; i++)
            {
                if (ParkingSpots[i].OccupiedBy == car)
                {
                    ParkingSpots[i].OccupiedBy = null;
                    ParkingSpots[i+1].OccupiedBy = null;
                    ParkingSpots[i].IsEmpty = true;
                    ParkingSpots[i + 1].IsEmpty = true;
                    break;
                }
            };
        }
    }


}