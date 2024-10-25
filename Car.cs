namespace ParkingDeluxe
{
    public class Car : Vehicle
    {
        public bool IsElectric;
        public Car(RegistrationNumber regNumber, string color, bool isElectric) : base(regNumber, color)
        {
            IsElectric = isElectric;
        }
    }
}