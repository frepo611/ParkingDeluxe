
namespace ParkingDeluxe
{
    public class Car : Vehicle
    {
        public bool IsElectric;
        public Car(RegistrationNumber regNumber, string color, bool isElectric) : base(regNumber, color)
        {
            IsElectric = isElectric;
        }

        public override void Park(List<ParkingSpot> parkingSpace)
        {
            int parkingSpotIndex = 0;
            for (parkingSpotIndex = 0; parkingSpotIndex < parkingSpace.Count; parkingSpotIndex += 2)
            {
                if (parkingSpace[parkingSpotIndex].IsEmpty && parkingSpace[parkingSpotIndex + 1].IsEmpty)
                    break;
            }
            parkingSpace[parkingSpotIndex].OccupiedBy = this;
            parkingSpace[parkingSpotIndex + 1].OccupiedBy = this;
            parkingSpace[parkingSpotIndex].IsEmpty = false;
            parkingSpace[parkingSpotIndex + 1].IsEmpty = false;



        }
    }
}