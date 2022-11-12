using UnityEngine;
using UnityEngine.UI;

public class DistanceToTarget : MonoBehaviour
{

    public Slider sliderDistanceToTargetX;
    public Slider sliderDistanceToTargetZ;
    public Transform vehicleTrasnform;
    private void Start()
    {
        transform.position = vehicleTrasnform.position + new Vector3(sliderDistanceToTargetX.value, 0, sliderDistanceToTargetZ.value);

        sliderDistanceToTargetX.onValueChanged.AddListener((v) =>
        {
            transform.position = vehicleTrasnform.position + new Vector3(v, 0, sliderDistanceToTargetZ.value);
        });

        sliderDistanceToTargetZ.onValueChanged.AddListener((v) =>
        {
            transform.position = vehicleTrasnform.position + new Vector3(sliderDistanceToTargetX.value, 0, v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
