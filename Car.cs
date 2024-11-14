namespace ParkingDeluxe;

public class Car : Vehicle
{
    public override string TypeName => "Bil";
    public bool IsElectric;
    private const int _size = 2;

    public Car(RegistrationNumber regNumber, string color, bool isElectric) : base(regNumber, color, _size)
    {
        IsElectric = isElectric;
    }
}
