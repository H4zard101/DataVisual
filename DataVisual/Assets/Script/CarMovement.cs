using UnityEngine;

public class CarMovement : MonoBehaviour
{

    // Top Speed
    public float Speed;
    public float Literacy;
    public bool isMoving;

    // Money For Country
    public float AverageMonthlyIncome;
    public float CostPerLiter;
    public float refuelCost;
    // Fuel Limit
    public float FuelConsumption;
    public float FuelTotal;

    public MoveCamera moveCamera;

    public GameObject car;
    public Rigidbody rb;

    private void Start()
    {
        moveCamera = FindObjectOfType<MoveCamera>();

        Speed = (Literacy / 10);     
        isMoving = true;
    }
    // Update is called once per frame
    void Update()
    {
        // when the transition is over then move the cars
            // MOVE THE CAR
            FuelTotal -= FuelConsumption * Time.deltaTime;

            if (FuelTotal <= 0)
            {
                isMoving = false;              
            }

            else if (FuelTotal >= 0)
            {
                isMoving = true;              
            }
      
        if (isMoving)
        {
            rb.AddForce(-Vector3.forward * Speed * Time.deltaTime);
        }
        else
        {
            Refuel();
        }

    }

    void Refuel()
    {
        refuelCost = CostPerLiter * 80;
        FuelTotal = CostPerLiter * 80;
        AverageMonthlyIncome -= refuelCost;
    }
}
