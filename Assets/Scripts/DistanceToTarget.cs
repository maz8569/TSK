using UnityEngine;
using UnityEngine.UI;

public class DistanceToTarget : MonoBehaviour
{

    public Slider sliderDistanceToTargetX;
    public Slider sliderDistanceToTargetY;
    public Slider sliderDistanceToTargetZ;
    public Transform vehicleTrasnform;
    private void Start()
    {
        transform.position = vehicleTrasnform.position + new Vector3(sliderDistanceToTargetX.value, sliderDistanceToTargetY.value, sliderDistanceToTargetZ.value);

        sliderDistanceToTargetX.onValueChanged.AddListener((v) =>
        {
            transform.position = vehicleTrasnform.position + new Vector3(v, sliderDistanceToTargetY.value, sliderDistanceToTargetZ.value);
        });
        sliderDistanceToTargetY.onValueChanged.AddListener((v) =>
        {
            transform.position = vehicleTrasnform.position + new Vector3(sliderDistanceToTargetX.value, v, sliderDistanceToTargetZ.value);
        });
        sliderDistanceToTargetZ.onValueChanged.AddListener((v) =>
        {
            transform.position = vehicleTrasnform.position + new Vector3(sliderDistanceToTargetX.value, sliderDistanceToTargetY.value, v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
