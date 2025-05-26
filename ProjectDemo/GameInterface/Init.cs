namespace ProjectDemo.GameInterface;

public abstract class Init<E> where E : struct
{
    protected E CurrentOption;
    protected bool IsGameVictory = true;

    private int _startInterfaceTitleAdjustX;
    private int _startInterfaceTileIntervalY;
    private int _playGameTextIntervalY;
    private int _endGameTextIntervalY;
    private int _gameOptionTitleIntervalY;
    private int _game1TextIntervalY;
    private int _game2TextIntervalY;
    private int _game3TextIntervalY;
    private int _game4TextIntervalY;
    private int _endInterfaceTitle1IntervalY;
    private int _endInterfaceTitle2IntervalY;
    private int _backMenuTextIntervalY;
    private int _endGameTextIntervalYCopy;


    // 开始界面 Text位置(x, y)间隔
    protected int StartInterfaceTitleAdjustX { get => _startInterfaceTitleAdjustX; set => _startInterfaceTitleAdjustX = value - 1; }
    protected int StartInterfaceTileIntervalY { get => _startInterfaceTileIntervalY; set => _startInterfaceTileIntervalY = value + 5; }
    protected int PlayGameTextIntervalX { get; set; }
    protected int PlayGameTextIntervalY { get => _playGameTextIntervalY; set => _playGameTextIntervalY = value + 8; }
    protected int EndGameTextIntervalX { get; set; }
    protected int EndGameTextIntervalY { get => _endGameTextIntervalY; set => _endGameTextIntervalY = value + 10; }
    // 游戏选择界面 Text位置(x, y)间隔
    protected int GameOptionTitleIntervalX { get; set; }
    protected int GameOptionTitleIntervalY { get => _gameOptionTitleIntervalY; set => _gameOptionTitleIntervalY = value + 5; }
    protected int Game1TextIntervalX { get; set; }
    protected int Game1TextIntervalY { get => _game1TextIntervalY; set => _game1TextIntervalY = value + 7; }
    protected int Game2TextIntervalX { get; set; }
    protected int Game2TextIntervalY { get => _game2TextIntervalY; set => _game2TextIntervalY = value + 9; }
    protected int Game3TextIntervalX { get; set; }
    protected int Game3TextIntervalY { get => _game3TextIntervalY; set => _game3TextIntervalY = value + 11; }
    protected int Game4TextIntervalX { get; set; }
    protected int Game4TextIntervalY { get => _game4TextIntervalY; set => _game4TextIntervalY = value + 13; }
    // 结束界面 Text位置(x, y)间隔
    protected int EndInterfaceTitle1IntervalX { get; set; }
    protected int EndInterfaceTitle1IntervalY { get => _endInterfaceTitle1IntervalY; set => _endInterfaceTitle1IntervalY = value + 5; }
    protected int EndInterfaceTitle2IntervalX { get; set; }
    protected int EndInterfaceTitle2IntervalY { get => _endInterfaceTitle2IntervalY; set => _endInterfaceTitle2IntervalY = value + 5; }
    protected int BackMenuTextIntervalX { get; set; }
    protected int BackMenuTextIntervalY { get => _backMenuTextIntervalY; set => _backMenuTextIntervalY = value + 8; }
    protected int EndGameTextIntervalXCopy { get; set; }
    protected int EndGameTextIntervalYCopy { get => _endGameTextIntervalYCopy; set => _endGameTextIntervalYCopy = value + 10; }
    // Text 汇总
    protected string StartInterfaceTitle => "小游戏集合";
    protected string GameOptionTitle => "游戏列表";
    protected string EndInterfaceTitle1 => "游戏胜利";
    protected string EndInterfaceTitle2 => "游戏失败";
    protected string PlayGameText => "开始游戏";
    protected string EndGameText => "退出游戏";
    protected string BackMenuText => "返回菜单";
    protected string Game1Text => "1.营救公主 ";
    protected string Game2Text => "2.飞行棋  ";
    protected string Game3Text => "3.贪吃蛇  ";
    protected string Game4Text => "4.俄罗斯方块";

    /// <summary>
    /// 计算 Text 在终端中间的坐标
    /// </summary>
    protected abstract void TextsMidpoint();
    /// <summary>
    /// 把 Text 显示在终端上
    /// </summary>
    protected abstract void DisplayTexts();
    /// <summary>
    /// 高亮显示当前选项
    /// </summary>
    protected abstract void HighlightSelection();
    /// <summary>
    /// 选择处理
    /// </summary>
    /// <returns> 返回状态 </returns>
    public abstract State SelectProcessing();
}