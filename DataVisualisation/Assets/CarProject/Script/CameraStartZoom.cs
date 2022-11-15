using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraStartZoom : MonoBehaviour
{
    public Camera camera;
    public Transform startPoint;
    public Transform endPoint;
    public bool isMoving = false;
    public int cameraSpeed = 1;
    public bool isLinedUp = false;

    public Vector3 CameraRotation;
    public Vector3 CameraPosition;
    public CameraObrbit cameraObrbit;
    public Button button1;
    public Button button2;
    public Button button3;

    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;

    public float cloud1Speed = 1;
    public float cloud2Speed = 1;
    public float cloud3Speed = 1;
    public float cloud4Speed = 1;

    public bool cloudMoveUp;
    public bool cloudMoveDown;
    public bool cloudMoveLeft;
    public bool cloudMoveRight;

    // Start is called before the first frame update
    void Start()
    {
        cameraObrbit = FindObjectOfType<CameraObrbit>();

        cloud1.SetActive(false);
        cloud2.SetActive(false);
        cloud3.SetActive(false);
        cloud4.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
        {
            if(!isLinedUp)
            {
                camera.transform.position = startPoint.position;
                isLinedUp = true;
            }

            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);

            cloud1.SetActive(true);
            cloud2.SetActive(true);
            cloud3.SetActive(true);
            cloud4.SetActive(true);

            cameraObrbit.enabled = false;
            camera.transform.rotation = Quaternion.Euler(CameraRotation);
            cameraSpeed++;
            camera.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, cameraSpeed * Time.deltaTime);

            moveTopCloud();
            moveBottomCloud();
            moveLeftCloud();
            moveRightCloud();
        }
        else
        {
            cameraSpeed = 0;
        }
    }

    public void moveTopCloud()
    {
        //cloud1
        if(cloudMoveDown)
        {
            cloud1.transform.Translate(-Vector3.forward * cloud1Speed * Time.deltaTime);
        }
        if(cloud1.transform.position.y <= 5)
        {
            cloudMoveDown = false;
        }
        
    }
    public void moveBottomCloud()
    {
        //cloud2
        if(cloudMoveUp)
        {
            cloud2.transform.Translate(Vector3.forward * cloud2Speed * Time.deltaTime);
        }
        if(cloud2.transform.position.y >= -4.4)
        {
            cloudMoveUp = false;
        }
    }
    public void moveLeftCloud()
    {
        //cloud3
        if(cloudMoveRight)
        {
            cloud3.transform.Translate(Vector3.right * cloud3Speed * Time.deltaTime);
        }
        if(cloud3.transform.position.x >= -8)
        {
            cloudMoveRight = false;
        }
    }
    public void moveRightCloud()
    {
        //cloud4
        if(cloudMoveLeft)
        {
            cloud4.transform.Translate(-Vector3.right * cloud4Speed * Time.deltaTime);
        }
        if(cloud4.transform.position.x <= 6.5)
        {
            cloudMoveLeft = false;
        }
    }
}
