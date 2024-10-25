namespace ParkingDeluxe
{
    public class RegistrationNumber
    {
        private char[] _letters = new char[3];
        private int _numbers;
        public string RegNumber => $"{_letters}{_numbers}";
    }
}