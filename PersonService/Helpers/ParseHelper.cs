namespace PersonService.Helpers;

public class ParseHelper
{
    public static bool IsNumber(string line)
    {
        return int.TryParse(line, out _);
    }
}