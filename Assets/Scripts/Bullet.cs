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
        if(isShoot && isSimulationActive)
            transform.position += speed * Time.deltaTime * transform.forward;
    }

    private void OnShoot(object data)
    {
        Debug.Log("Shoot");
        isShoot = true;
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

}
