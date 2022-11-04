using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MoveCamera : MonoBehaviour
{

    public Camera camera;
    public Transform startPoint;
    public Transform endPoint;
    public float speed =1f;

    public Button startButton;
    public GameObject pannel;
    public GameObject pausePannel;


    public GameObject cloud1;
    public Transform cloud1StartPoint;
    public Transform cloud1EndPoint;


    public GameObject cloud2;
    public Transform cloud2StartPoint;
    public Transform cloud2EndPoint;

    public GameObject cloud3;
    public Transform cloud3StartPoint;
    public Transform cloud3EndPoint;

    public GameObject cloud4;
    public Transform cloud4StartPoint;
    public Transform cloud4EndPoint;


    public int cloudSpeed = 1;
    public bool move_Clouds = false;
    void Start()
    {
        startButton.gameObject.SetActive(false);
        pannel.SetActive(false);
        pausePannel.SetActive(false);
        move_Clouds = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveCamera();
        moveClouds();
    }

    private void Update()
    {
        StartCoroutine(showUi());
        StartCoroutine(cloudDelay());
        StartCoroutine(cloudHidden());

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePannel.SetActive(true);
        }
    }
    IEnumerator showUi()
    {
        yield return new WaitForSeconds(2f);

        startButton.gameObject.SetActive(true);
        pannel.SetActive(true);

    }

    void moveCamera()
    {

        speed++;
        camera.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, speed * Time.deltaTime);
    }

    void moveClouds()
    {
        if(move_Clouds)
        {
            cloud1.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
            cloud2.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
            cloud3.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
            cloud4.transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
        }

    }
    IEnumerator cloudDelay()
    {
        yield return new WaitForSeconds(1f);
        moveClouds();
    }

    IEnumerator cloudHidden()
    {
        yield return new WaitForSeconds(5f);
        move_Clouds = false;
        cloud1.SetActive(false);
        cloud2.SetActive(false);
        cloud3.SetActive(false);
        cloud4.SetActive(false);
    }
}
