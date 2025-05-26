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
            switch (state)
            {
                case State.Start:
                    state = start.SelectProcessing();
                    break;
                case State.Run:
                    state = run.SelectProcessing();
                    break;
                case State.End:
                    state = end.SelectProcessing();
                    break;
            }
        }
    }
}