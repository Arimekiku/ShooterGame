using System;
using UnityEngine;

public class PauseInput : GameInput
{
    public event Action OnEscapePressed;
    
    public PauseInput(IInputSwitcher inputSwitcher) : base(inputSwitcher) { }

    public override void EnterInput()
    {
        Time.timeScale = 0f;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputSwitcher.SwitchInput<PlayerInput>();
            OnEscapePressed.Invoke();
        }
    }
}
