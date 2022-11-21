using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandlerMonement : MonoBehaviour
{

    public Transform toCopy;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toCopy)
        {
            transform.rotation = toCopy.rotation * Quaternion.Euler(offset);
        }
    }
}
