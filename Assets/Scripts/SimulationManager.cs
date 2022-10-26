using UnityEngine;

public class SimulationManager : MonoBehaviour
{

    public GameObject simulationParams;
    public GameObject simulationStopButtons;

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

}
