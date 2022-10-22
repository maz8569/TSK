using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speedX = 0.0f;
    public float speedZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  new Vector3(speedX, 0, speedZ) * Time.deltaTime;
    }
}
