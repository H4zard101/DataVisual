using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionConfirmed : MonoBehaviour
{

    public CarMovement carMovement;


    public void  Start()
    {
        carMovement = FindObjectOfType<CarMovement>();
    }
    public void buttonClick()
    {
        carMovement.isSelectionConfirmed = true;
    }
}
