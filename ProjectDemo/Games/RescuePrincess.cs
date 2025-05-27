using System.Reflection.Metadata;
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
    private (int x, int y) _princessLocation;
    private int _playerHp = 100;
    private int _bossHp = 100;
    private string _victoryText1 = "你战胜了 Boss，快去营救公主吧";
    private string _victoryText2 = "前往公主身边按 J 键营救公主";
    private string _failText = "你被 Boss 打败了，公主被 Boss 抓走了";
    private bool _isGameOver = false;
    private bool _isGeneratePrincess = true;

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
                case Role.Princess:
                    _minRandomY = ConsoleWindow.Init.Miny + 1;
                    _maxRandomY = ConsoleWindow.Init.MiddleBar1 / 2;
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

    private void DrawRole(bool isVictory = false)
    {
        if (isVictory)
        {
            var princess = new RoleProperties(Role.Princess);
            _princessLocation = princess.Location;
            Auxiliary.Display(princess.Princess, ConsoleColor.Cyan, princess.Location.x, princess.Location.y);
            return;
        }

        var player = new RoleProperties(Role.Player);
        var boss = new RoleProperties(Role.Boss);
        _playerLocation = player.Location;
        _bossLocation = boss.Location;
        Auxiliary.Display(player.Player, ConsoleColor.Yellow, player.Location.x, player.Location.y);
        Auxiliary.Display(boss.Boss, ConsoleColor.Green, boss.Location.x, boss.Location.y);
    }

    private bool PlayerMoveScope(int x, int y)
    {
        if (x <= ConsoleWindow.Init.Minx || x >= ConsoleWindow.Init.MaxX ||
            y <= ConsoleWindow.Init.Miny || y >= ConsoleWindow.Init.MiddleBar1)
        {
            return false;
        }
        if (x == _bossLocation.x && y == _bossLocation.y)
        {
            return false;
        }
        if (_isGameOver && (x == _princessLocation.x && y == _princessLocation.y))
        {
            return false;
        }
        return true;
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
            Auxiliary.Display(" ", ConsoleColor.Yellow, oldPlayerX, oldPlayerY);
            Auxiliary.Display(Round, ConsoleColor.Yellow, x, y);
            _playerLocation = (x, y);
        }
    }

    private (bool isGameOver, Role? role) IsGameOver()
    {
        if (_playerHp <= 0 || _bossHp <= 0)
        {
            if (_playerHp <= 0 && _bossHp <= 0)
            {
                Role who = _playerHp > _bossHp ? Role.Player : Role.Boss;
                return (true, who);
            }
            return (true, _bossHp <= 0 ? Role.Player : Role.Boss);
        }
        return (false, null);
    }

    private bool IsBossBeside()
    { 
        if (_playerLocation.x == _bossLocation.x && 
            (_playerLocation.y == _bossLocation.y - 1 || _playerLocation.y == _bossLocation.y + 1))
        {
            return true;
        }
        else if (_playerLocation.y == _bossLocation.y && 
                 (_playerLocation.x == _bossLocation.x - 1 || _playerLocation.x == _bossLocation.x + 1))
        {
            return true;
        }
        return false;
    }

    private bool IsPrincessBeside()
    {
        if (_playerLocation.x == _princessLocation.x && 
            (_playerLocation.y == _princessLocation.y - 1 || _playerLocation.y == _princessLocation.y + 1))
        {
            return true;
        }
        else if (_playerLocation.y == _princessLocation.y && 
                 (_playerLocation.x == _princessLocation.x - 1 || _playerLocation.x == _princessLocation.x + 1))
        {
            return true;
        }
        return false;
    }

    private (int playerAtkHarm, int bossAtkHarm) PlayerBossAtk()
    {
        int playerAtk = Random.Shared.Next(1, 11);
        int bossAtk = Random.Shared.Next(1, 11);
        _bossHp -= playerAtk;
        _playerHp -= bossAtk;
        return (playerAtk, bossAtk);
    }

    private void DisplayFightingText(int playerAtkHarm, int bossAtkHarm)
    {
        string titleText = "开始和 Boss 战斗，按 J 键继续";
        string playerHpText = $"你对 Boss 造成{playerAtkHarm}点伤害，Boss 血量为{_bossHp}";
        string bossHpText = $"Boss 对你造成{bossAtkHarm}点伤害，你血量为{_playerHp}";

        Auxiliary.Display(titleText, ConsoleColor.White, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 1);
        Auxiliary.Display(playerHpText, ConsoleColor.Yellow, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 2);
        Auxiliary.Display(bossHpText, ConsoleColor.Green, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 3);
    }

    private void ClearFightingText()
    {
        for (var i = 1; i <= 3; i++)
        {
            for (var x = ConsoleWindow.Init.Minx + 1; x <= ConsoleWindow.Init.MaxX - 2; x++)
            {
                Auxiliary.Display(" ", ConsoleColor.White, x, ConsoleWindow.Init.MiddleBar1 + i);
            }
        }
    }

    public override void Run()
    {
        Console.Clear();
        DrawWalls();
        DrawMiddleBar();
        DrawRole();
        int isClearText = -1;

        while (true)
        {
            var (isGameOver, role) = IsGameOver();
            if (isGameOver)
            { 
                _isGameOver = true;
                ClearFightingText();
                if (role == Role.Player)
                {
                    Auxiliary.Display(" ", ConsoleColor.Yellow, _bossLocation.x, _bossLocation.y);
                    Auxiliary.Display(_victoryText1, ConsoleColor.Green, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 1);
                    Auxiliary.Display(_victoryText2, ConsoleColor.Green, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 2);
                    _bossLocation = (0, 0);
                    if (_isGeneratePrincess)
                    { 
                        DrawRole(true);
                        _isGeneratePrincess = false;
                    }
                }
                else if (role == Role.Boss)
                {
                    Auxiliary.Display(_failText, ConsoleColor.Red, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 1);
                    Thread.Sleep(3000);
                    break;
                }
            }

            var key = Console.ReadKey(true);
            int x = _playerLocation.x;
            int y = _playerLocation.y;

            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    PlayerMove(x, y, Move.Up);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    PlayerMove(x, y, Move.Down);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    PlayerMove(x, y, Move.Left);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    PlayerMove(x, y, Move.Right);
                    break;
                case ConsoleKey.J:
                    if (IsBossBeside())
                    {
                        if (isClearText >= 1) ClearFightingText();
                        var (playerAtk, bossAtk) = PlayerBossAtk();
                        DisplayFightingText(playerAtk, bossAtk);
                        isClearText++;
                    }
                    else if (_isGameOver && IsPrincessBeside())
                    {
                        ClearFightingText();
                        Auxiliary.Display("你成功营救了公主，游戏结束", ConsoleColor.Cyan, ConsoleWindow.Init.Minx + 1, ConsoleWindow.Init.MiddleBar1 + 1);
                        Thread.Sleep(2000);
                        return;
                    }
                    break;
            }
        }
    }
}