namespace ParkingDeluxe;

public class RegistrationNumber
{
    private readonly int _numbers;
    private readonly string _letters;
    public string RegNumber => $"{_letters}{_numbers:D3}";
    public RegistrationNumber(string letters, int numbers)
    {
        _numbers = numbers;
        _letters = letters;
    }
public static RegistrationNumber GetRandom()
    {
        int numbers = Random.Shared.Next(1000);

        string swedishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";
        string letters = "";
        for (int i = 0; i < 3; i++)
        {
            char RandomLetter = swedishAlphabet[Random.Shared.Next(swedishAlphabet.Length)];
            letters = $"{letters}{RandomLetter}";
        }

        return new RegistrationNumber(letters, numbers);
    }
}