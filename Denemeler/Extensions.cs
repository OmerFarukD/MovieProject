

namespace Denemeler;

public static class Extensions
{

    public static bool StartsWithA(this string text)
    {
        return text.StartsWith('A');
    }


    public static string AddText(this string text , string value)
    {
        return $"{text} {value.ToUpper()}"; 
    }
}
