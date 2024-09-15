using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Kite;
    public Vector3 cameraOffset;
    public static CameraFollow  Instance;
    
    void Update()
    {
        if (Kite != null)
            transform.position = Kite.position + cameraOffset;
    }
}
