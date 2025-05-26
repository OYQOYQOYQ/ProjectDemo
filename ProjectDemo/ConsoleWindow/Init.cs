using ProjectDemo.Tools;

namespace ProjectDemo.ConsoleWindow;

public static class Init
{
    public static int WindowWidth => 50;
    public static int WindowHeight => 30;
    public static int Minx;
    public static int MaxX;
    public static int Miny;
    public static int MaxY;
    public static int MiddleBar1 => WindowHeight - 4;
    public static int MiddleBar2 => WindowHeight - 10;

    static Init()
    {
        Console.CursorVisible = false;
        (Minx, Miny) = Auxiliary.CalculateMidpoint(WindowWidth, WindowHeight);
        MaxX = Minx + WindowWidth - 2;
        MaxY = Miny + WindowHeight - 1;
    }
}