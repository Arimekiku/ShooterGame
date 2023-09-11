using System.Collections.Generic;
using System.Linq;

public class InputProvider
{
    private readonly List<GameInput> _inputs;

    public InputProvider(List<GameInput> inputs)
    {
        _inputs = inputs;
    }

    public GameInput GetInput<T>() where T : GameInput
    {
        GameInput newGameInput = _inputs.First(i => i is T);

        return newGameInput;
    }
}