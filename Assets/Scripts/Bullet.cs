using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public Slider initialSpeed;
    private UnityAction<object> onShoot; 
    private UnityAction<object> onSimulationStateChange;
    public bool isShoot; 
    private bool isSimulationActive = false;

    public float speed = 0.0f;
    public float timeSpend;

    public SimulationManager simulationManager;

    public Vector3 initalPosition; 


    private void Awake()
    {
        onShoot = new UnityAction<object>(OnShoot);
        onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Shoot", onShoot);
        EventManager.StartListening("SimulationState", onSimulationStateChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Shoot", onShoot);
        EventManager.StopListening("SimulationState", onSimulationStateChange);
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
            timeSpend += Time.deltaTime;

            //Debug.Log(Formulas.Formulas.GetPosition(timeSpend));

            //transform.position += speed * Time.deltaTime * transform.forward;
            //transform.position += Formulas.Formulas.GetPosition(timeSpend) * 0.01f;
            transform.position = initalPosition + new Vector3(
                                                (float)Formulas.Formulas.GetPositionZ(timeSpend),
                                                (float)Formulas.Formulas.GetPositionY(timeSpend),
                                                (float)Formulas.Formulas.GetPositionX(timeSpend));

        }

        if (isShoot && transform.position.y <= -1)
        {
            simulationManager.StopSimulation();
            isShoot = false;
        }
    }

    private void OnShoot(object data)
    {
        Debug.Log("Shoot");
        isShoot = true;
        transform.parent = null;
        initalPosition = transform.position;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit " + other.name);
        isShoot = false;
        simulationManager.StopSimulation();
    }

}
