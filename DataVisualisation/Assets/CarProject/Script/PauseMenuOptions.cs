using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOptions : MonoBehaviour
{
    public MoveCamera moveCamera;


    private void Start()
    {
        moveCamera = FindObjectOfType<MoveCamera>();        
    }
    public void GlobeButton()
    {
        moveCamera.CameraForward = false;
        moveCamera.pausePannel.SetActive(false);
        moveCamera.cloudUpMoveIn = true;
        moveCamera.cloudDownMoveIn = true;
        moveCamera.cloudRightMoveIn = true;
        moveCamera.cloudLeftMoveIn = true;
        moveCamera.isShowing = false;
        moveCamera.switchScene = true;
    }
}
