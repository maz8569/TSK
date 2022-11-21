using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAiming : MonoBehaviour
{
    Vector2 p1;
    Vector2 p2;

    public float rotateSpeed = 10.0f;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float zoomChangeAmount = 80.0f;
    // Start is called before the first frame update
    void Start()
    {
        p1 = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed * Time.deltaTime;
            float dy = (p2 - p1).y * rotateSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x - dy, transform.eulerAngles.y + dx, transform.eulerAngles.z);
            p1 = p2;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            virtualCamera.m_Lens.FieldOfView -= zoomChangeAmount * Time.deltaTime * 10f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            virtualCamera.m_Lens.FieldOfView += zoomChangeAmount * Time.deltaTime * 10f;
        }

        virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, 5f, 60f);
    }
}
