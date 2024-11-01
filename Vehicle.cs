namespace ParkingDeluxe;

public abstract class Vehicle
{
    public RegistrationNumber RegistrationNumber { get;}
    public string Color { get; }
    public Vehicle(RegistrationNumber regNumber, string color)
    {
        RegistrationNumber = regNumber;   
        Color = color;
    }
    public abstract void Park(List<ParkingSpot> parkingSpace);
}