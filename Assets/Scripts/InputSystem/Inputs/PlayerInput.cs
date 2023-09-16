using System;
using UnityEngine;

public class PlayerInput : GameInput
{
    public event Action OnEscapePressed;
    public event Action<float> OnMovePressed;

    public PlayerInput(IInputSwitcher inputSwitcher) : base(inputSwitcher) { }

    public override void EnterInput()
    {
        Time.timeScale = 1f;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputSwitcher.SwitchInput<PauseInput>();
            OnEscapePressed.Invoke();
        }
    }

    public override void FixedUpdate()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        
        OnMovePressed.Invoke(horizontalMove);
    }
}
