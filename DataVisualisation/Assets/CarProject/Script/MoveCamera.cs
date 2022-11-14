using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MoveCamera : MonoBehaviour
{
    // Camera
    public Camera camera;
    public Transform startPoint;
    public Transform endPoint;
    public float cameraSpeedMovingAway =1f;
    public float cameraSpeedMovingIn = 1f;
    public bool CameraForward = true;


    // UI
    public Button startButton;
    public GameObject pannel;
    public GameObject pausePannel;
    public bool isShowing = false;
    public static bool gameIsPaused = false;


    // Clouds
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public float cloudSpeed;

    public bool cloudUpMove;
    public bool cloudDownMove;
    public bool cloudRightMove;
    public bool cloudLeftMove;

    public bool cloudUpMoveIn;
    public bool cloudDownMoveIn;
    public bool cloudRightMoveIn;
    public bool cloudLeftMoveIn;

    public bool switchScene;



    // make sure the ui gets reset and the move out of the way
    void Start()
    {
        startButton.gameObject.SetActive(false);
        pannel.SetActive(false);
        pausePannel.SetActive(false);       
        StartCoroutine(showUi());
        CameraForward = true;
        cloudUpMove = true;
        cloudDownMove = true;
        cloudRightMove = true;
        cloudLeftMove = true;
    }


    void FixedUpdate()
    { 
        if(CameraForward)
        {
            cameraSpeedMovingIn = 0;
            moveCameraBack();
        }

        if(!CameraForward)
        {
            cameraSpeedMovingAway = 0;
            moveCameraForward();
        }

        if(CameraForward)
        {
            moveTopCloudAway();
            moveBottomCloudAway();
            moveRightCloudAway();
            moveLeftCloudAway();
        }

        if(!CameraForward)
        {
            moveTopCloudBack();
            moveBottomCloudBack();
            moveRightCloudBack();
            moveLeftCloudBack();
        }


    }

    private void Update()
    {
        if(isShowing)
        {
            startButton.gameObject.SetActive(true);
            pannel.SetActive(true);
        }

        else if(!isShowing)
        {
            startButton.gameObject.SetActive(false);
            pannel.SetActive(false);
        }    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(switchScene)
        {
            StartCoroutine(NextScene());
        }
    }

    public void moveTopCloudAway()
    {
        if(cloudUpMove)
        {
            cloud3.transform.Translate(-Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if(cloud3.transform.position.x >= 15.0F)
        {
            cloudUpMove = false;
        }
    }
    public void moveBottomCloudAway()
    {
        if (cloudDownMove)
        {
            cloud4.transform.Translate(-Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud4.transform.position.x <= -15.0F)
        {
            cloudDownMove = false;
        }
    }
    public void moveRightCloudAway()
    {
        if (cloudRightMove)
        {
            cloud1.transform.Translate(-Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud1.transform.position.z <= -15.0F)
        {
            cloudRightMove = false;
        }
    }
    public void moveLeftCloudAway()
    {
        if (cloudLeftMove)
        {
            cloud2.transform.Translate(-Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud2.transform.position.z >= 15.0F)
        {
            cloudLeftMove = false;
        }
    }

    public void moveTopCloudBack()
    {
        if (cloudUpMoveIn)
        {
            cloud4.transform.Translate(Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud4.transform.position.x >= -0.51)
        {
            cloudUpMoveIn = false;
        }
    }
    public void moveBottomCloudBack()
    {
        if (cloudDownMoveIn)
        {
            cloud3.transform.Translate(Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud3.transform.position.x <= 1.38f)
        {
            cloudDownMoveIn = false;
        }
    }
    public void moveRightCloudBack()
    {
        if (cloudRightMoveIn)
        {
            cloud2.transform.Translate(Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud2.transform.position.z <= 1)
        {
            cloudRightMoveIn = false;
        }
    }
    public void moveLeftCloudBack()
    {
        if (cloudLeftMoveIn)
        {
            cloud1.transform.Translate(Vector3.forward * cloudSpeed * Time.deltaTime);
        }

        if (cloud1.transform.position.z >= -1.32)
        {
            cloudLeftMoveIn = false;
        }
    }

    // pause menu
    void Resume()
    {
        pausePannel.SetActive(false);
        isShowing = true;
        gameIsPaused = false;
    }
    // pause menu
    void Pause()
    {
        pausePannel.SetActive(true);
        isShowing = false;
        gameIsPaused = true;
    }

    // buttons/ panel
    IEnumerator showUi()
    {
        yield return new WaitForSeconds(2f);
        isShowing = true;

    }
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Here");

    }

    void moveCameraBack()
    {
        cameraSpeedMovingAway++;
        camera.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, cameraSpeedMovingAway * Time.deltaTime);     
    }

    public void moveCameraForward()
    {
        cameraSpeedMovingIn++;
        camera.transform.position = Vector3.Lerp(endPoint.position, startPoint.position, cameraSpeedMovingIn * Time.deltaTime);
    }

}
