using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionConfirmed : MonoBehaviour
{

    public CarMovement[] carMovement;


    public void  Start()
    {
        carMovement = FindObjectsOfType<CarMovement>();
    }
    public void buttonClick()
    {
        for (int i = 0; i < carMovement.Length; i++)
        {
            carMovement[i].isSelectionConfirmed = true;
        }
       
    }
}
