using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveOverlayToView : MonoBehaviour
{
    public bool isMoving = false;
    public Vector2 onScreenPositions;
    public Vector2 offScreenPositions;
    public RectTransform pannelPos;

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            pannelPos.anchoredPosition = onScreenPositions;
        }
        else if(!isMoving)
        {
            pannelPos.anchoredPosition = offScreenPositions;
        }
    }
}
