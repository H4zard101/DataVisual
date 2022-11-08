using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCarMoving : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Debug.Log("Car");
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.GetComponent<CarMovement>().isMoving = false;
            other.GetComponent<CarMovement>()._Wage = 0;
        }
    }
}
