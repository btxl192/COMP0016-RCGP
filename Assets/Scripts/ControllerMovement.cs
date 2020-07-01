using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{

    private const bool test = true;

    private Transform controller;

    void Start()
    {
        controller = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            //move controller according to mouse
            Quaternion rot = controller.rotation;
            controller.rotation = Quaternion.Euler(rot.eulerAngles.x + Input.GetAxis("Mouse Y"),
                                                         rot.eulerAngles.y + Input.GetAxis("Mouse X"),
                                                         rot.eulerAngles.z);
        }
        else
        {
            //move controller according to oculus controller
            Vector3 controllerRot = (OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote).eulerAngles);
            controller.rotation = Quaternion.Euler(-controllerRot.x, controllerRot.y, -controllerRot.z);
        }
    }
}
