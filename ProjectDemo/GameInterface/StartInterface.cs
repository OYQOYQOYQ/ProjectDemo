using ProjectDemo.Tools;


namespace ProjectDemo.GameInterface;

public enum StartOption
{
    StartGame,
    EndGame,
}

public class StartInterface : Init<StartOption>
{
    protected override void TextsMidpoint()
    {
        var (titleX, titleY) = Auxiliary.CalculateTextMidpoint(StartInterfaceTitle);
        var (option1X, option1Y) = Auxiliary.CalculateTextMidpoint(PlayGameText);
        var (option2X, option2Y) = Auxiliary.CalculateTextMidpoint(EndGameText);

        StartInterfaceTitleAdjustX = titleX;
        StartInterfaceTileIntervalY = titleY;
        PlayGameTextIntervalX = option1X;
        PlayGameTextIntervalY = option1Y;
        EndGameTextIntervalX = option2X;
        EndGameTextIntervalY = option2Y;
    }

    protected override void DisplayTexts()
    {
        Auxiliary.Display(StartInterfaceTitle, ConsoleColor.White, StartInterfaceTitleAdjustX, StartInterfaceTileIntervalY);
        Auxiliary.Display(PlayGameText, ConsoleColor.Red, PlayGameTextIntervalX, PlayGameTextIntervalY);
        Auxiliary.Display(EndGameText, ConsoleColor.White, EndGameTextIntervalX, EndGameTextIntervalY);
    }

    protected override void HighlightSelection()
    {
        switch (CurrentOption)
        {
            case StartOption.StartGame:
                Auxiliary.Display(PlayGameText, ConsoleColor.Red, PlayGameTextIntervalX, PlayGameTextIntervalY);
                Auxiliary.Display(EndGameText, ConsoleColor.White, EndGameTextIntervalX, EndGameTextIntervalY);
                break;
            case StartOption.EndGame:
                Auxiliary.Display(PlayGameText, ConsoleColor.White, PlayGameTextIntervalX, PlayGameTextIntervalY);
                Auxiliary.Display(EndGameText, ConsoleColor.Red, EndGameTextIntervalX, EndGameTextIntervalY);
                break;
        }
    }

    public override State SelectProcessing()
    {
        TextsMidpoint();
        DisplayTexts();
        CurrentOption = StartOption.StartGame;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    CurrentOption = StartOption.StartGame;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    CurrentOption = StartOption.EndGame;
                    break;
                case ConsoleKey.Enter:
                    if (CurrentOption is StartOption.StartGame) return State.Run;
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            HighlightSelection();
        }
    }
}