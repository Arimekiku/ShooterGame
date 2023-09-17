public interface IGameStateSwitcher
{
    public void SwitchState<T>() where T : GameState;
}