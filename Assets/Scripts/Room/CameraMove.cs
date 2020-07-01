using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        //use arrow keys to move camera
        transform.rotation = Quaternion.Euler(rot.x - Input.GetAxisRaw("Vertical"), 
                                              rot.y + Input.GetAxisRaw("Horizontal"), 
                                              rot.z);

    }
}
