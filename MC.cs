﻿namespace ParkingDeluxe
{
    public class MC : Vehicle
    {
        public string Brand { get; }
        public MC(RegistrationNumber regNumber, string color, string brand) : base(regNumber, color)
        {
            Brand = brand;
        }
    }
}