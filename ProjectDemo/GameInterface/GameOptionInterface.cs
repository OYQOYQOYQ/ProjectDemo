using System.Runtime.InteropServices;
using ProjectDemo.Tools;

namespace ProjectDemo.GameInterface;

public enum GameOption
{
    Game1,
    Game2,
    Game3,
    Game4,
}

public class GameOptionInterface : Init<GameOption>
{
    protected override void TextsMidpoint()
    {
        var (titleX, titleY) = Auxiliary.CalculateTextMidpoint(GameOptionTitle);
        var (option1X, option1Y) = Auxiliary.CalculateTextMidpoint(Game1Text);
        var (option2X, option2Y) = Auxiliary.CalculateTextMidpoint(Game2Text);
        var (option3X, option3Y) = Auxiliary.CalculateTextMidpoint(Game3Text);
        var (option4X, option4Y) = Auxiliary.CalculateTextMidpoint(Game4Text);

        GameOptionTitleIntervalX = titleX;
        GameOptionTitleIntervalY = titleY;
        Game1TextIntervalX = option1X;
        Game1TextIntervalY = option1Y;
        Game2TextIntervalX = option2X;
        Game2TextIntervalY = option2Y;
        Game3TextIntervalX = option3X;
        Game3TextIntervalY = option3Y;
        Game4TextIntervalX = option4X;
        Game4TextIntervalY = option4Y;
    }

    protected override void DisplayTexts() 
    { 
        Auxiliary.Display(GameOptionTitle, ConsoleColor.White, GameOptionTitleIntervalX, GameOptionTitleIntervalY);
        Auxiliary.Display(Game1Text, ConsoleColor.Red, Game1TextIntervalX, Game1TextIntervalY);
        Auxiliary.Display(Game2Text, ConsoleColor.White, Game2TextIntervalX, Game2TextIntervalY);
        Auxiliary.Display(Game3Text, ConsoleColor.White, Game3TextIntervalX, Game3TextIntervalY);
        Auxiliary.Display(Game4Text, ConsoleColor.White, Game4TextIntervalX, Game4TextIntervalY);
    }

    protected override void HighlightSelection() 
    {
        switch (CurrentOption)
        {
            case GameOption.Game1:
                Auxiliary.Display(Game1Text, ConsoleColor.Red, Game1TextIntervalX, Game1TextIntervalY);
                Auxiliary.Display(Game2Text, ConsoleColor.White, Game2TextIntervalX, Game2TextIntervalY);
                Auxiliary.Display(Game3Text, ConsoleColor.White, Game3TextIntervalX, Game3TextIntervalY);
                Auxiliary.Display(Game4Text, ConsoleColor.White, Game4TextIntervalX, Game4TextIntervalY);
                break;
            case GameOption.Game2:
                Auxiliary.Display(Game1Text, ConsoleColor.White, Game1TextIntervalX, Game1TextIntervalY);
                Auxiliary.Display(Game2Text, ConsoleColor.Red, Game2TextIntervalX, Game2TextIntervalY);
                Auxiliary.Display(Game3Text, ConsoleColor.White, Game3TextIntervalX, Game3TextIntervalY);
                Auxiliary.Display(Game4Text, ConsoleColor.White, Game4TextIntervalX, Game4TextIntervalY);
                break;
            case GameOption.Game3:
                Auxiliary.Display(Game1Text, ConsoleColor.White, Game1TextIntervalX, Game1TextIntervalY);
                Auxiliary.Display(Game2Text, ConsoleColor.White, Game2TextIntervalX, Game2TextIntervalY);
                Auxiliary.Display(Game3Text, ConsoleColor.Red, Game3TextIntervalX, Game3TextIntervalY);
                Auxiliary.Display(Game4Text, ConsoleColor.White, Game4TextIntervalX, Game4TextIntervalY);
                break;
            case GameOption.Game4:
                Auxiliary.Display(Game1Text, ConsoleColor.White, Game1TextIntervalX, Game1TextIntervalY);
                Auxiliary.Display(Game2Text, ConsoleColor.White, Game2TextIntervalX, Game2TextIntervalY);
                Auxiliary.Display(Game3Text, ConsoleColor.White, Game3TextIntervalX, Game3TextIntervalY);
                Auxiliary.Display(Game4Text, ConsoleColor.Red, Game4TextIntervalX, Game4TextIntervalY);
                break;
        }
    }

    public override State SelectProcessing()
    {
        TextsMidpoint();
        DisplayTexts();
        CurrentOption = GameOption.Game1;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    bool minOption = GameOption.Game1 >= CurrentOption--; 
                    CurrentOption = minOption ? GameOption.Game1 : CurrentOption--;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    bool maxOption = GameOption.Game4 >= CurrentOption++;
                    CurrentOption = maxOption ? CurrentOption++ : GameOption.Game4;
                    break;
                case ConsoleKey.Enter:
                    return State.End;
            }
            HighlightSelection();
        }
    }
}