using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class TooltipScreenSpaceUI : MonoBehaviour {

    public static TooltipScreenSpaceUI Instance { get; private set; }



    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private System.Func<string> getTooltipTextFunc;

    private void Awake() {
        Instance = this;

        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();


        HideTooltip();
        //TestTooltip();
    }

    private void SetText(string tooltipText) {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void TestTooltip() {
        ShowTooltip("Testing tooltip...");

        FunctionPeriodic.Create(() => {
            string abc = "qwertyuiopasdfghjklOQWEERTYSDVPIOSDFNMLM\n\n\n\n\n\n\n\n\n";
            string text = "Subscribe to Code Monkey! ";
            for (int i = 0; i < Random.Range(5, 200); i++) {
                text += abc[Random.Range(0, abc.Length)];
            }
            ShowTooltip(text);
        }, .5f);
    }

    private void Update() {
        SetText(getTooltipTextFunc());

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            // Tooltip left screen on right side
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            // Tooltip left screen on top side
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string tooltipText) {
        ShowTooltip(() => tooltipText);
    }

    private void ShowTooltip(System.Func<string> getTooltipTextFunc) {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }


    private void HideTooltip() {
        gameObject.SetActive(false);
    }


    public static void ShowTooltip_Static(string tooltipText) {
        Instance.ShowTooltip(tooltipText);
    }

    public static void ShowTooltip_Static(System.Func<string> getTooltipTextFunc) {
        Instance.ShowTooltip(getTooltipTextFunc);
    }

    public static void HideTooltip_Static() {
        Instance.HideTooltip();
    }


}
