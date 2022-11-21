using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ManualVehicleMovement : MonoBehaviour
{
    public float speed = 0.0f;

    public Slider sliderSpeed;

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

    void Update()
    {
        if (isSimulationActive)
        {
            float z = Input.GetAxisRaw("Horizontal");
            float x = Input.GetAxisRaw("Vertical");

            transform.position += Time.deltaTime * speed * new Vector3(x, 0, -z) * TimeSpeed;
        }
    }

    private void OnSimulationStateChange(object data)
    {
        isSimulationActive = (bool)data;

        if (isSimulationActive)
        {
            if (sliderSpeed)
            {
                speed = sliderSpeed.value;
            }

        }

    }

    private void OnSimulationSpeedChange(object data)
    {
        TimeSpeed = (float)data;
    }
}
