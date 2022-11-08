using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButtons : MonoBehaviour
{
    public RectTransform flag_ImageSize;
    public float initalWidth = 200;
    public float initalHeight = 200;
    public float smallerWidth = 100;
    public float smallerHeight = 100;

    public Button statsButton;
    public Button confirmButton;
    public void ShowMoreButtons()
    {
        // Shirnk the current flag
        flag_ImageSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, smallerWidth);
        flag_ImageSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, smallerHeight);

        // make 2 buttons appear
        statsButton.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);

    }

    public void Start()
    {
        // sets the size of the image
        flag_ImageSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initalWidth);
        flag_ImageSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, initalHeight);

        // set the buttons to be invisible on start
        statsButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }
}
