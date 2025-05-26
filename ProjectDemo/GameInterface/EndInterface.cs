using ProjectDemo.Tools;


namespace ProjectDemo.GameInterface;

public enum EndOption
{
    BackMenu,
    EndGame,
}

public class EndInterface : Init<EndOption>
{
    protected override void TextsMidpoint()
    {
        var (title1X, title1Y) = Auxiliary.CalculateTextMidpoint(EndInterfaceTitle1);
        var (title2X, title2Y) = Auxiliary.CalculateTextMidpoint(EndInterfaceTitle2);
        var (option1X, option1Y) = Auxiliary.CalculateTextMidpoint(BackMenuText);
        var (option2X, option2Y) = Auxiliary.CalculateTextMidpoint(EndGameText);

        EndInterfaceTitle1IntervalX = title1X;
        EndInterfaceTitle1IntervalY = title1Y;
        EndInterfaceTitle2IntervalX = title2X;
        BackMenuTextIntervalX = option1X;
        BackMenuTextIntervalY = option1Y;
        EndGameTextIntervalXCopy = option2X;
        EndGameTextIntervalYCopy = option2Y;
    }

    protected override void DisplayTexts()
    {
        if (IsGameVictory){
            Auxiliary.Display(EndInterfaceTitle1, ConsoleColor.White, EndInterfaceTitle1IntervalX, EndInterfaceTitle1IntervalY);
        } else { 
            Auxiliary.Display(EndInterfaceTitle2, ConsoleColor.White, EndInterfaceTitle2IntervalX, EndInterfaceTitle2IntervalY);
        }
        Auxiliary.Display(BackMenuText, ConsoleColor.Red, BackMenuTextIntervalX, BackMenuTextIntervalY);
        Auxiliary.Display(EndGameText, ConsoleColor.White, EndGameTextIntervalXCopy, EndGameTextIntervalYCopy);
    }

    protected override void HighlightSelection()
    {
        switch (CurrentOption)
        {
            case EndOption.BackMenu:
                Auxiliary.Display(BackMenuText, ConsoleColor.Red, BackMenuTextIntervalX, BackMenuTextIntervalY);
                Auxiliary.Display(EndGameText, ConsoleColor.White, EndGameTextIntervalXCopy, EndGameTextIntervalYCopy);
                break;
            case EndOption.EndGame:
                Auxiliary.Display(BackMenuText, ConsoleColor.White, BackMenuTextIntervalX, BackMenuTextIntervalY);
                Auxiliary.Display(EndGameText, ConsoleColor.Red, EndGameTextIntervalXCopy, EndGameTextIntervalYCopy);
                break;
        }
    }

    public override State SelectProcessing()
    {
        TextsMidpoint();
        DisplayTexts();
        CurrentOption = EndOption.BackMenu;
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    CurrentOption = EndOption.BackMenu;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    CurrentOption = EndOption.EndGame;
                    break;
                case ConsoleKey.Enter:
                    if (CurrentOption is EndOption.BackMenu) return State.Start;
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            HighlightSelection();
        }
    }
}