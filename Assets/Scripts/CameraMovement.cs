using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform Target;
    Vector3 offset;

    public void Start()
    {
        offset = transform.position - Target.position;
    }

    public void LateUpdate()
    {
        transform.position = offset + Target.position;
    }

}
