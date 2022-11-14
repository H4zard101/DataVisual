using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider slider = null;

    public void Next()
    {
        slider.value = slider.value + 1;
        slider.onValueChanged.Invoke(slider.value);
    }

    public void Previous()
    {
        slider.value = slider.value - 1;
        slider.onValueChanged.Invoke(slider.value);
    }
}
