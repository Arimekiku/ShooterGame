using UnityEngine;

public class InputBehaviour : MonoBehaviour, IInputSwitcher
{
    private DataProvider<GameInput> _inputProvider;
    private GameInput _currentInput;
    private IUpdatable _updatableInput;
    private IFixedUpdatable _fixedUpdatableInput;

    private bool _inputDisabled;
    
    public void Init(GameInput startInput, DataProvider<GameInput> inputProvider, PlayerBehaviour player)
    {
        _inputProvider = inputProvider;
        _currentInput = startInput;
        _currentInput.EnterInput();
        
        _updatableInput = _currentInput;
        _fixedUpdatableInput = _currentInput;
        
        player.OnDeath += DisableInput;
    }

    private void Update()
    {
        if (_inputDisabled)    
            return;
        
        _updatableInput?.Update();
    }

    private void FixedUpdate()
    {
        if (_inputDisabled)
            return;
        
        _fixedUpdatableInput?.FixedUpdate();
    }

    private void DisableInput()
    {
        _inputDisabled = true;
    }

    public void EnableInput()
    {
        _inputDisabled = false;
    }

    public void SwitchInput<T>() where T : GameInput
    {
        _currentInput = _inputProvider.GetObjectOfType<T>();
        _currentInput.EnterInput();
        
        _updatableInput = _currentInput;
        _fixedUpdatableInput = _currentInput;
    }
}