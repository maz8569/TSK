using UnityEngine;
using UnityEngine.UI;

public class BulletHandler : MonoBehaviour
{
    public Slider sliderAngleZ;

    // Start is called before the first frame update
    void Start()
    {
        sliderAngleZ.onValueChanged.AddListener((v) =>
        {
            transform.localRotation = Quaternion.Euler(0, -90, v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
