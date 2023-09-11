using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextReader : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(UpdateText);
        
        UpdateText(_slider.value);
    }

    private void UpdateText(float value)
    {
        _textField.text = _slider.value.ToString(CultureInfo.InvariantCulture);
    }
}