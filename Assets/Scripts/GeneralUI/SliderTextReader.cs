using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderTextReader : MonoBehaviour
{
    [FormerlySerializedAs("_textField")] [SerializeField] private Text TextField;
    [FormerlySerializedAs("_slider")] [SerializeField] private Slider Slider;

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