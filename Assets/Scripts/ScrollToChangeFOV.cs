using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToChangeFOV : MonoBehaviour
{
    public float zoomChangeAmount = 80.0f;

    private Camera cameraComp;

    private void Start()
    {
        cameraComp = GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cameraComp.fieldOfView -= zoomChangeAmount * Time.deltaTime * 10f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            cameraComp.fieldOfView += zoomChangeAmount * Time.deltaTime * 10f;
        }

        cameraComp.fieldOfView = Mathf.Clamp(cameraComp.fieldOfView, 5f, 150f);

    }
}
