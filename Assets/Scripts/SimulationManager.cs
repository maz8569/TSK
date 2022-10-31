using UnityEngine;

public class SimulationManager : MonoBehaviour
{

    public GameObject simulationParams;
    public GameObject simulationStopButtons;

    [Space]
    [Header("Simualted Objects")]
    public GameObject MainVehicle;
    public GameObject Target;
    public GameObject Bullet;

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
        Debug.Log("Simulation has stoped");
    }

    public void FireShot()
    {
        EventManager.TriggerEvent("Shoot", null);
        Debug.Log("Shoot");
    }

    public void ResetSimulation()
    {
        StopSimulation();
        MainVehicle.transform.position = new Vector3(-1, 0.25f, -3);
        DistanceToTarget dst = Target.GetComponent<DistanceToTarget>();
        Target.transform.position = MainVehicle.transform.position + new Vector3(dst.sliderDistanceToTargetX.value, 0, dst.sliderDistanceToTargetZ.value);
        Bullet bullet = Bullet.GetComponent<Bullet>();
        Bullet.transform.position = new Vector3(0, 0, 0);
        bullet.isShoot = false;
        Debug.Log("Reset");
    }

}
