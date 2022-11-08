using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowOverlay : MonoBehaviour
{
    MoveOverlayToView moveOverlay;


    // pannel info
    public string country;
    public float literacy;
    public float energy;
    public float monthlyIncome;

    // pannel objects
    public TextMeshProUGUI country_;
    public TextMeshProUGUI literacy_;
    public TextMeshProUGUI energy_;
    public TextMeshProUGUI monthlyIncome_;

    public void Start()
    {
        moveOverlay = FindObjectOfType<MoveOverlayToView>();
    }
    public void TriggerOverlay()
    {
        moveOverlay.isMoving = true;
        country_.SetText(country);
        literacy_.SetText(literacy.ToString() + "%");
        energy_.SetText(energy.ToString() + "%");
        monthlyIncome_.SetText("£" + monthlyIncome.ToString());
    }
    public void RemoveOverlay()
    {
        moveOverlay.isMoving = false;
    }
}
