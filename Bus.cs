namespace ParkingDeluxe
{
    public class Bus : Vehicle
    {
        public int NoOfPassengers { get; }
        public Bus(RegistrationNumber regNumber, string color, int noOfPassengers) :base(regNumber, color)
        {
            NoOfPassengers = noOfPassengers;
        }
    }
}