using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class VehicleMovement : MonoBehaviour
{
    public float speedX = 0.0f;
    public float speedZ = 0.0f;

    public Slider sliderX;
    public Slider sliderZ;

    private bool isSimulationActive = false;

    private UnityAction<object> onSimulationStateChange;
    private void Awake()
    {
        onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
    }

    private void OnEnable()
    {
        EventManager.StartListening("SimulationState", onSimulationStateChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SimulationState", onSimulationStateChange);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSimulationActive)
        {
            transform.position += new Vector3(speedX, 0, speedZ) * Time.deltaTime;
        }
    }

    private void OnSimulationStateChange(object data)
    {
        isSimulationActive = (bool)data;

        if (isSimulationActive)
        {
            if (sliderX)
            {
                speedX = sliderX.value;
            }

            if (sliderZ)
            {
                speedZ = sliderZ.value;
            }

        }

    }

}
