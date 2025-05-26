namespace ProjectDemo.Games;

public abstract class Init
{
    protected static string Round => "●";
    protected static string Blocks => "■";
    protected static string Star => "★";

    protected abstract void DrawWalls();
    public abstract void Run();
}