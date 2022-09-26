using System.Text;
using System;

public static class StringGenerationHelper
{
    public static string GetRandomString(int length, bool toLower = false)
    {
        var resultStringBuilder = new StringBuilder();
        var random = new Random();

        char randomLetter;
        for (int i = 0; i < length; i++)
        {
            var flt = random.NextDouble();
            var shift = Convert.ToInt32(Math.Floor(25 * flt));
            randomLetter = Convert.ToChar(shift + 65);

            resultStringBuilder.Append(randomLetter);
        }

        var resultString = resultStringBuilder.ToString();
        if (toLower)
        {
            resultString = resultString.ToLower();
        }
        return resultString;
    }
}
