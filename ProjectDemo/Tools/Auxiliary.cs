using ProjectDemo.ConsoleWindow;

namespace ProjectDemo.Tools;

internal static class Auxiliary
{
    public static void Display(string str, ConsoleColor color, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    public static (int midpointX, int midpointY) CalculateMidpoint(int specifyW, int specifyH)
    {
        int midpointX = (Console.WindowWidth - specifyW) / 2;
        int midpointY = (Console.WindowHeight - specifyH) / 2;
        return (midpointX, midpointY);
    }

    public static (int textX, int textY) CalculateTextMidpoint(string text)
    {
        var (midpointX, midpointY) = CalculateMidpoint(Init.WindowWidth, Init.WindowHeight);
        int textX = (midpointX + Init.WindowWidth / 2) - (text.Length / 2);
        return (textX, midpointY);
    }
}