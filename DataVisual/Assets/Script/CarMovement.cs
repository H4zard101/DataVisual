using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{


    public MoveCamera moveCamera;

    public GameObject car;
    public Rigidbody rb;
    public Button confirmSelection;


    private float _Speed;
    public float _Wage;
    public float _Consuption;

    public bool isMoving;

    public float literacy;

    public bool isSelectionConfirmed;

    private void Start()
    {
        _Speed = literacy / 10;
        isMoving = true;
        isSelectionConfirmed = false;

    }
    // Update is called once per frame
    void Update()
    {
        if(isSelectionConfirmed)
        {
            _Wage -= _Consuption * Time.deltaTime;


            if (_Wage <= 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            if (isMoving)
            {
                rb.AddForce(-Vector3.forward * _Speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }


  

    }


}
