public class LevelPerformState : GameState
{
    private PlayerBehaviour _player;

    public LevelPerformState(PlayerBehaviour player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.EnablePlayer();
    }

    public override void Exit()
    {
        _player.DisablePlayer();
    }
}