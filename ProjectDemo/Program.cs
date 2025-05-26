using ProjectDemo.GameInterface;

namespace ProjectDemo;

public enum State
{
    Start,
    Run,
    End,
}

class Program
{
    static void Main()
    {
        var state = State.Start;
        var start = new StartInterface();
        var run = new GameOptionInterface();
        var end = new EndInterface();

        while (true)
        {
            Console.Clear();
            state = state switch
            {
                State.Start => start.SelectProcessing(),
                State.Run => run.SelectProcessing(),
                State.End => end.SelectProcessing(),
                _ => state
            };
        }
    }
}