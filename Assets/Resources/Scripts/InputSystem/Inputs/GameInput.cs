public abstract class GameInput : IData, IUpdatable, IFixedUpdatable
{
    protected readonly IInputSwitcher _inputSwitcher;

    protected GameInput(IInputSwitcher inputSwitcher)
    {
        _inputSwitcher = inputSwitcher;
    }

    public abstract void EnterInput();
    
    public abstract void Update();

    public abstract void FixedUpdate();
}