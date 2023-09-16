using UnityEngine;

public class InputBehaviour : MonoBehaviour, IInputSwitcher
{
    private DataProvider<GameInput> _inputProvider;
    private IUpdatable _updatableInput;
    private IFixedUpdatable _fixedUpdatableInput;

    public void Init(GameInput startInput, DataProvider<GameInput> inputProvider)
    {
        _inputProvider = inputProvider;
        startInput.EnterInput();
        
        _updatableInput = startInput;
        _fixedUpdatableInput = startInput;
    }

    private void Update()
    {
        _updatableInput?.Update();
    }

    private void FixedUpdate()
    {
        _fixedUpdatableInput?.FixedUpdate();
    }
    
    public void SwitchInput<T>() where T : GameInput
    {
        T seekingInput = _inputProvider.GetObjectOfType<T>();
        seekingInput.EnterInput();
        
        _updatableInput = seekingInput;
        _fixedUpdatableInput = seekingInput;
    }
}