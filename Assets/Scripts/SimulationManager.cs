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
    public GameObject[] cameras;

    [Space]
    [Header("Simualted Objects")]
    public GameObject MainVehicle;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject ShootingCamera;
    public GameObject BulletHolder;
    [Space]
    public Slider SimulationSpeed;

    public WallMeshGenerator wall;

    [SerializeField] private Transform spheresParent;

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

        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
        cameras[2].gameObject.SetActive(false);
        cameras[3].gameObject.SetActive(false);

        Debug.Log("Simulation has started");
    }

    public void StopSimulation()
    {
        EventManager.TriggerEvent("SimulationState", false);
        simulationParams.SetActive(true);
        simulationStopButtons.SetActive(false);

        crosshair.SetActive(false);
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
        cameras[3].gameObject.SetActive(false);

        Debug.Log("Simulation has stoped");
    }

    public void FireShot()
    {
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(false);
        cameras[2].gameObject.SetActive(false);
        cameras[3].gameObject.SetActive(true);

        SimulationSpeed.value = 0.05f;

        EventManager.TriggerEvent("Shoot", null);
        Debug.Log("Shoot");
        //ActivateMainCamera();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && simulationStopButtons.activeInHierarchy)
        {
            crosshair.SetActive(!crosshair.activeInHierarchy);
            if (crosshair.activeInHierarchy)
            {
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(true);
                cameras[3].gameObject.SetActive(false);
                BulletHolder.transform.parent = ShootingCamera.transform;
            }
            else
            {
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                cameras[2].gameObject.SetActive(false);
                cameras[3].gameObject.SetActive(false);
                BulletHolder.transform.parent = MainVehicle.transform;
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
        wall.GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in spheresParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Reset");
    }

}
