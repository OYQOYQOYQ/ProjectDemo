using System.Runtime.InteropServices;
using ProjectDemo.Tools;

namespace ProjectDemo.Games;

enum Role
{
    Player,
    Boss,
    Princess
}

enum Move
{ 
    Up,
    Down,
    Left,
    Right
}

public class RescuePrincess : Init
{
    private readonly string _wall = Blocks;
    private (int x, int y) _playerLocation;
    private (int x, int y) _bossLocation;

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
        }

        for (var y = ConsoleWindow.Init.Miny; y <= ConsoleWindow.Init.MaxY; y++)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, ConsoleWindow.Init.Minx, y);
            Auxiliary.Display(_wall, ConsoleColor.Red, ConsoleWindow.Init.MaxX, y);
        }
    }

    protected void DrawMiddleBar()
    {
        for (var x = ConsoleWindow.Init.Minx; x <= ConsoleWindow.Init.MaxX; x += 2)
        {
            Auxiliary.Display(_wall, ConsoleColor.Red, x, ConsoleWindow.Init.MiddleBar1);
        }
    }

    private void DrawRole()
    {
        var player = new RoleProperties(Role.Player);
        var boss = new RoleProperties(Role.Boss);
        _playerLocation = player.Location;
        _bossLocation = boss.Location;
        Auxiliary.Display(player.Player, ConsoleColor.Green, player.Location.x, player.Location.y);
        Auxiliary.Display(boss.Boss, ConsoleColor.Red, boss.Location.x, boss.Location.y);
    }

    private bool PlayerMoveScope(int x, int y)
    {
        if (x <= ConsoleWindow.Init.Minx || x >= ConsoleWindow.Init.MaxX ||
            y <= ConsoleWindow.Init.Miny || y >= ConsoleWindow.Init.MiddleBar1)
        {
            return false;
        }
        return (x != _bossLocation.x) || (y != _bossLocation.y);
    }

    private void PlayerMove(int x, int y, Move move)
    {
        int oldPlayerX = x;
        int oldPlayerY = y;
        switch (move)
        {
            case Move.Up:
                y--;
                break;
            case Move.Down:
                y++;
                break;
            case Move.Left:
                x--;
                break;
            case Move.Right:
                x++;
                break;
        }

        if (PlayerMoveScope(x, y))
        {
            Auxiliary.Display(" ", ConsoleColor.White, oldPlayerX, oldPlayerY);
            Auxiliary.Display(Round, ConsoleColor.Green, x, y);
            _playerLocation = (x, y);
        }
    }

    public override void Run()
    {
        Console.Clear();
        DrawWalls();
        DrawMiddleBar();
        DrawRole();

        while (true)
        {
            var key = Console.ReadKey(true);
            int x = _playerLocation.x;
            int y = _playerLocation.y;

            switch (key.Key)
            {
                case ConsoleKey.W:
                    PlayerMove(x, y, Move.Up);
                    break;
                case ConsoleKey.S:
                    PlayerMove(x, y, Move.Down);
                    break;
                case ConsoleKey.A:
                    PlayerMove(x, y, Move.Left);
                    break;
                case ConsoleKey.D:
                    PlayerMove(x, y, Move.Right);
                    break;
            }
        }
    }
}