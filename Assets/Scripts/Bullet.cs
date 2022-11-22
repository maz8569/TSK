using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    private Statistics statistics = null;

    public Slider initialSpeed;
    private UnityAction<object> onShoot; 
    private UnityAction<object> onSimulationStateChange;
    private UnityAction<object> onSimulationSpeedChange;
    public bool isShoot; 
    public bool isSimulationActive = false;

    public float speed = 0.0f;
    public float timeSpend;

    public float TimeSpeed = 1.0f;

    public SimulationManager simulationManager;

    public Vector3 initalPosition; 


    private void Awake()
    {
        onShoot = new UnityAction<object>(OnShoot);
        onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
        onSimulationSpeedChange = new UnityAction<object>(OnSimulationSpeedChange);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Shoot", onShoot);
        EventManager.StartListening("SimulationState", onSimulationStateChange);
        EventManager.StartListening("SimulationSpeedChanged", onSimulationSpeedChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Shoot", onShoot);
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
        if (isShoot && isSimulationActive)
        {
            timeSpend += Time.deltaTime * TimeSpeed;

            //Debug.Log(Formulas.Formulas.GetPosition(timeSpend));

            //transform.position += speed * Time.deltaTime * transform.forward;
            //transform.position += Formulas.Formulas.GetPosition(timeSpend) * 0.01f;
            transform.position = initalPosition + new Vector3(
                                                (float)Formulas.Formulas.GetPositionZ(timeSpend),
                                                (float)Formulas.Formulas.GetPositionY(timeSpend),
                                                (float)Formulas.Formulas.GetPositionX(timeSpend));

            statistics.Add(timeSpend, 
                            transform.position,
                            Formulas.Formulas.GetVelocity(timeSpend),
                            Formulas.Formulas.GetAcceleration(timeSpend)
                            );
        }

        if (isShoot && transform.position.y <= -1)
        {
            StopSimulation();
            isShoot = false;
            EventManager.TriggerEvent("Hit", null);
        }
    }

    private void OnShoot(object data)
    {
        statistics = new Statistics();
        isShoot = true;
        transform.parent = null;
        initalPosition = transform.position;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnSimulationStateChange(object data)
    {
        isSimulationActive = (bool)data;

        if (isSimulationActive)
        {
            if (initialSpeed)
            {
                speed = initialSpeed.value;
            }

        }

    }

    private void OnSimulationSpeedChange(object data)
    {
        TimeSpeed = (float)data;
    }

    private void OnTriggerEnter(Collider other)
    {
        isShoot = false;
        Debug.Log("hit " + other.name);
        EventManager.TriggerEvent("Hit", null);
        simulationManager.StopSimulation();
        StopSimulation();
    }

    private void StopSimulation()
    {
        statistics.Save();
        simulationManager.StopSimulation();
    }
}
