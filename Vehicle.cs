using System.Drawing;

namespace ParkingDeluxe;

public class Vehicle
{
    public RegistrationNumber RegistrationNumber { get;}
    public string Color { get; }
    public Vehicle(RegistrationNumber regNumber, string color)
    {
        RegistrationNumber = regNumber;   
        Color = color;
    }
}