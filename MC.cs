
namespace ParkingDeluxe
{
    public class MC : Vehicle
    {
        public string Brand { get; }
        private const int _size = 1;
        public MC(RegistrationNumber regNumber, string color, string brand) : base(regNumber, color, _size)
        {
            Brand = brand;
        }
    }
}