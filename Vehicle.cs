namespace ParkingDeluxe;

public abstract class Vehicle : ITimed
{
    public RegistrationNumber RegistrationNumber { get;}
    public abstract string TypeName { get; }
    public string Color { get; }
    public int Size { get; }
    private DateTime _parkingStartTime;
    private DateTime _parkingEndTime;
    public Vehicle(RegistrationNumber regNumber, string color, int size)
    {
        RegistrationNumber = regNumber;   
        Color = color;
        Size = size;

    }
    public virtual string Describe()
    {   
        if (this is null)
        { 
            return String.Empty;
        }
        string misc = this switch
        {
            Car c => c.IsElectric ? "Elbil" : "Ingen elbil",
            MC m => m.Brand,
            Bus b => $"{b.NoOfPassengers} passagerare",
            _ => throw new InvalidOperationException("Unexpected vehicle type")
        };

        return $"{this.RegistrationNumber.RegNumber,-18} {this.TypeName,-10} {this.Color,-8}  {misc,-20}";
    }
    public override string ToString()
    {
        return $"{this.Color} {this.TypeName} ({this.RegistrationNumber.RegNumber})";
    }

    public void StartTimer()
    {
        _parkingStartTime = DateTime.Now;
    }
    public void EndTimer()
    {
        _parkingEndTime = DateTime.Now;
    }
    public TimeSpan GetElapsedTime()
    {
        return _parkingEndTime - _parkingStartTime;
    }

}