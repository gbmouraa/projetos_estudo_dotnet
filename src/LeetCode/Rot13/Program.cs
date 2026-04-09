// Desafio: https://www.codewars.com/kata/530e15517bc88ac656000716

using System.Text;

Console.WriteLine(Kata.Rot13("Test"));

public class Kata
{
    public static string Rot13(string message)
    {
        // seria cheat ja definir o alfabeto em rot13? rsrs
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        StringBuilder result = new StringBuilder();

        foreach (char c in message)
        {
            if (!alphabet.Contains(c.ToString().ToUpper()))
            {
                result.Append(c);
                continue;
            }

            int currentLetterIndex = alphabet.IndexOf(c.ToString().ToUpper());

            if (currentLetterIndex + 13 > 25)
            {
                int rot13Index = currentLetterIndex + 13 - 26;

                if (c.ToString() != c.ToString().ToUpper())
                    result.Append(alphabet[rot13Index].ToString().ToLower());
                else
                    result.Append(alphabet[rot13Index]);

                continue;
            }

            if (c.ToString() != c.ToString().ToUpper())
                result.Append(alphabet[currentLetterIndex + 13].ToString().ToLower());
            else
                result.Append(alphabet[currentLetterIndex + 13]);
        }

        return result.ToString();
    }
}