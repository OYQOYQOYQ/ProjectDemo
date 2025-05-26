using ProjectDemo.Tools;

namespace ProjectDemo.Games;

enum Role
{
    Player,
    Boss,
    Princess
}

public class RescuePrincess : Init
{
    private readonly string _wall = Blocks;

    private struct Window
    {
        public int minX;
        public int minY;
        public int maxX;
        public int maxY;

        public Window()
        {
            (minX, minY) = Auxiliary.CalculateMidpoint(ConsoleWindow.Init.WindowHeight, ConsoleWindow.Init.WindowHeight);
            maxX = minX + ConsoleWindow.Init.WindowWidth;
            maxY = minY + ConsoleWindow.Init.WindowHeight;
        }
    }

    private struct RoleProperties
    {
        private readonly int _minRandomX;
        private readonly int _maxRandomX;
        private readonly int _minRandomY;
        private readonly int _maxRandomY;

        public readonly string Player = Round;
        public readonly string Boss = Blocks;
        public readonly string Princess = Star;
        public (int x, int y) Location;

        public RoleProperties(Role role)
        {
            var window = new Window();
            _minRandomX = window.minX + 1;
            _maxRandomX = window.maxX - 1;
            switch (role)
            {
                case Role.Player:
                    _minRandomY = window.minY + 1;
                    _maxRandomY = (window.maxY - 1) / 2;
                    break;
                case Role.Boss:
                    _minRandomY = (window.maxY - 1) / 2;
                    _maxRandomY = window.maxY - 1;
                    break;
            }
            RandomLocation();
        }

        private void RandomLocation()
        {
            int x = Random.Shared.Next(_minRandomX, _maxRandomX);
            int y = Random.Shared.Next(_minRandomY, _maxRandomY);
            Location = (x, y);
        }
    }

    protected override void DrawWalls()
    {
        var window = new Window();
        for (var x = window.minX; x < window.maxX; x += 2)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, x, window.minY);
            Auxiliary.Display(_wall, ConsoleColor.Red, x, window.maxY);
        }

        for (var y = window.minY; y < window.maxY + 1; y++)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, window.minX, y);
            Auxiliary.Display(_wall, ConsoleColor.Red, window.maxX, y);
        }
    }

    private void DrawRole()
    {
        var player = new RoleProperties(Role.Player);
        var boss = new RoleProperties(Role.Boss);
        Auxiliary.Display(player.Player, ConsoleColor.Green, player.Location.x, player.Location.y);
        Auxiliary.Display(boss.Boss, ConsoleColor.Red, boss.Location.x, boss.Location.y);
    }

    public override void Run()
    {
        Console.Clear();
        DrawWalls();
        DrawRole();
        while (true)
        {
            // Game logic goes here
        }
    }
}