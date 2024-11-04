
namespace ParkingDeluxe;

public class Bus : Vehicle
{
    public int NoOfPassengers { get; }
    private const int _size = 4;
    public Bus(RegistrationNumber regNumber, string color, int noOfPassengers) :base(regNumber, color, _size)
    {
        NoOfPassengers = noOfPassengers;
    }
}