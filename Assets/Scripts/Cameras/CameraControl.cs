using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform Target;
    void Awake()
    {
        Target = FindObjectOfType<PlayerControl>().transform;
    }

    
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x,Target.position.y,transform.position.z) ;
    }
}
