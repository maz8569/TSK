using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    [Header("Canvas objects")]
    public GameObject simulationParams;
    public GameObject simulationStopButtons;
    public GameObject crosshair;

    [Header("Different cameras")]
    /// 0 - Main camera
    /// 1 - Aiming camera
    /// 2 - Results camera
    public Camera[] cameras;

    [Space]
    [Header("Simualted Objects")]
    public GameObject MainVehicle;
    public GameObject Target;
    public GameObject Bullet;
    [Space]
    public Slider SimulationSpeed;

    public WallMeshGenerator wall;

    private void Start()
    {
        EventManager.TriggerEvent("SimulationSpeedChanged", SimulationSpeed.value);

        SimulationSpeed.onValueChanged.AddListener((v) =>
        {
            EventManager.TriggerEvent("SimulationSpeedChanged", v);
        }
        );
    }

    public void StartSimulation()
    {
        EventManager.TriggerEvent("SimulationState", true);
        simulationParams.SetActive(false);
        simulationStopButtons.SetActive(true);
        Debug.Log("Simulation has started");
    }

    public void StopSimulation()
    {
        EventManager.TriggerEvent("SimulationState", false);
        simulationParams.SetActive(true);
        simulationStopButtons.SetActive(false);

        crosshair.SetActive(false);
        ActivateMainCamera();

        Debug.Log("Simulation has stoped");
    }

    public void FireShot()
    {
        EventManager.TriggerEvent("Shoot", null);
        Debug.Log("Shoot");
        ActivateMainCamera();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && simulationStopButtons.activeInHierarchy)
        {
            crosshair.SetActive(!crosshair.activeInHierarchy);
            if (crosshair.activeInHierarchy)
            {
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
            }
            else
            {
                ActivateMainCamera();
            }
        }
    }

    public void ActivateMainCamera()
    {
        cameras[1].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
    }

    public void ResetSimulation()
    {
        StopSimulation();
        MainVehicle.transform.position = new Vector3(-1, 0.25f, -3);
        DistanceToTarget dst = Target.GetComponent<DistanceToTarget>();
        Target.transform.position = MainVehicle.transform.position + new Vector3(dst.sliderDistanceToTargetX.value, 0, dst.sliderDistanceToTargetZ.value);
        Bullet bullet = Bullet.GetComponent<Bullet>();
        bullet.isShoot = false;
        bullet.timeSpend = 0;
        bullet.transform.parent = MainVehicle.transform;
        Bullet.transform.localPosition = new Vector3(0, 2.26f, 0);
        Bullet.transform.GetChild(0).gameObject.SetActive(false);
        wall.ClearVertices();
        Debug.Log("Reset");
    }

}
