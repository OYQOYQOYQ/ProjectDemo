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
            _minRandomX = ConsoleWindow.Init.Minx + 1;
            _maxRandomX = ConsoleWindow.Init.MaxX - 1;
            switch (role)
            {
                case Role.Player:
                    _minRandomY = ConsoleWindow.Init.Miny + 1;
                    _maxRandomY = ConsoleWindow.Init.MiddleBar1 / 2;
                    break;
                case Role.Boss:
                    _minRandomY = ConsoleWindow.Init.MiddleBar1 / 2;
                    _maxRandomY = ConsoleWindow.Init.MiddleBar1;
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
        for (var x = ConsoleWindow.Init.Minx; x <= ConsoleWindow.Init.MaxX; x += 2)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, x, ConsoleWindow.Init.Miny);
            Auxiliary.Display(_wall, ConsoleColor.Red, x, ConsoleWindow.Init.MaxY);
            Auxiliary.Display(_wall, ConsoleColor.Red, x, ConsoleWindow.Init.MiddleBar1);
        }

        for (var y = ConsoleWindow.Init.Miny; y <= ConsoleWindow.Init.MaxY; y++)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, ConsoleWindow.Init.Minx, y);
            Auxiliary.Display(_wall, ConsoleColor.Red, ConsoleWindow.Init.MaxX, y);
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