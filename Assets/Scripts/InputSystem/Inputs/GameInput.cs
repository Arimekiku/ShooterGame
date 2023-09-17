public abstract class GameInput : IData, IUpdatable, IFixedUpdatable
{
    protected readonly IInputSwitcher InputSwitcher;

    protected GameInput(IInputSwitcher inputSwitcher)
    {
        InputSwitcher = inputSwitcher;
    }

    public abstract void EnterInput();

    public abstract void Update();

    public virtual void FixedUpdate() { }
}