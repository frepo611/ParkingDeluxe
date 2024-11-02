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
    public override string ToString()
    {   
        string misc = this switch
        {
            Car c => c.IsElectric ? "Elbil" : "Ingen elbil",
            MC m => m.Brand,
            Bus b => $"{b.NoOfPassengers} passagerare",
            _ => throw new InvalidOperationException("Unexpected vehicle type")
        };

        return $"{this.RegistrationNumber.RegNumber,-8} {this.GetType().Name,-6} {this.Color,-8}  {misc}";
    }
}