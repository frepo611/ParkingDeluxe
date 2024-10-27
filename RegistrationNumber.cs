namespace ParkingDeluxe
{
    public class RegistrationNumber
    {
        private int _numbers;
        private string _letters;
        public string RegNumber => $"{_letters}{_numbers}";
        public RegistrationNumber(int numbers, string letters)
        {
            _numbers = numbers;
            _letters = letters;
        }
    }
}