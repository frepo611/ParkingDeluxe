﻿namespace ParkingDeluxe;

public abstract class Vehicle : ITimed
{
    public RegistrationNumber RegistrationNumber { get;}
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
    public string Describe()
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

        return $"{this.RegistrationNumber.RegNumber,-8} {this.GetType().Name,-6} {this.Color,-8}  {misc} {this._parkingStartTime.ToShortTimeString()}";
    }
    public override string ToString()
    {
        return $"{this.RegistrationNumber.RegNumber} {this.Color} {this.GetType().Name}";
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