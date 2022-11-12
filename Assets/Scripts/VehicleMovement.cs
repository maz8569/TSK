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
    public float TimeSpeed = 1.0f;

    private UnityAction<object> onSimulationStateChange; 
    private UnityAction<object> onSimulationSpeedChange;

    private void Awake()
    {
        onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
        onSimulationSpeedChange = new UnityAction<object>(OnSimulationSpeedChange);
    }

    private void OnEnable()
    {
        EventManager.StartListening("SimulationState", onSimulationStateChange);
        EventManager.StartListening("SimulationSpeedChanged", onSimulationSpeedChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SimulationState", onSimulationStateChange);
        EventManager.StopListening("SimulationSpeedChanged", onSimulationSpeedChange);
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
            transform.position += new Vector3(speedX, 0, speedZ) * Time.deltaTime * TimeSpeed;
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

    private void OnSimulationSpeedChange(object data)
    {
        TimeSpeed = (float)data;
    }

}
