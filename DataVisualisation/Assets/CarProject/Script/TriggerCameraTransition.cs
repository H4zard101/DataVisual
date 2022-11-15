using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraTransition : MonoBehaviour
{
    public CameraStartZoom cameraStartZoom;
    // Start is called before the first frame update
    void Start()
    {
        cameraStartZoom = FindObjectOfType<CameraStartZoom>();
    }

    public void Trigger()
    {

        cameraStartZoom.isMoving = true;
        cameraStartZoom.cloudMoveDown = true;
        cameraStartZoom.cloudMoveLeft = true;
        cameraStartZoom.cloudMoveRight = true;
        cameraStartZoom.cloudMoveUp = true;
    }
}
