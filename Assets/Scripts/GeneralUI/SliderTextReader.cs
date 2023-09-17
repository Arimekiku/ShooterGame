using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextReader : MonoBehaviour
{
    [SerializeField] private Text TextField;
    [SerializeField] private Slider Slider;

    private void Awake()
    {
        Slider.onValueChanged.AddListener(UpdateText);
        
        UpdateText(Slider.value);
    }

    private void UpdateText(float value)
    {
        TextField.text = Slider.value.ToString(CultureInfo.InvariantCulture);
    }
}