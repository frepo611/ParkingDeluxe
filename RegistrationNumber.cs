using System.Text.RegularExpressions;

namespace ParkingDeluxe;

public record RegistrationNumber // TODO record?
{
    private readonly int _numbers;
    private readonly string _letters;
    public string RegNumber => $"{_letters}{_numbers:D3}";

    public RegistrationNumber(string letters, int numbers)
    {
        _numbers = numbers;
        _letters = letters.ToUpper();
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

    internal static bool TryCreate(string userEnteredString, out RegistrationNumber? userEnteredRegNumber)
    {
        userEnteredRegNumber = null;
        if (IsValid(userEnteredString))
            {
                userEnteredRegNumber = new RegistrationNumber(userEnteredString[..3], int.Parse(userEnteredString[3..]));
                return true;
            }
        return false;
    }

    private static bool IsValid(string userEnteredString)
    {
        if (userEnteredString.Length != 6)
            return false;
        string regExPattern = @"^[A-Za-zåÅäÄöÖ]{3}\d{3}$";
        return Regex.IsMatch(userEnteredString, regExPattern);
    }
}
