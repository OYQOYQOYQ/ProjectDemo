namespace ProjectDemo.ConsoleWindow;

public static class Init
{
    public static int WindowWidth => 50;
    public static int WindowHeight => 30;
    public static int RightSideBar => WindowWidth - 2;
    public static int BottomBar => WindowHeight - 2;
    public static int MiddleBar1 => WindowHeight - 7;
    public static int MiddleBar2 => WindowHeight - 12;

    static Init() { Console.CursorVisible = false;}
}