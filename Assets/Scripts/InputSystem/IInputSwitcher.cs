public interface IInputSwitcher
{
    public void SwitchInput<T>() where T: GameInput;
}