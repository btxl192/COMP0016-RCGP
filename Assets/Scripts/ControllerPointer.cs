using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    public Material red;
    public Material green;

    private Renderer thisrenderer;
    private Transform controller;
    private bool authenticated = true;

    public static Vector3 pos { get; private set; }

    private void Start()
    {
        thisrenderer = GetComponent<Renderer>();
        controller = transform.parent;
        Events._Authenticated += CheckAuth;
    }

    private void Update()
    {
        if (authenticated)
        {
            ExecButton();
            //pos = transform.position;
        }
    }

    private void ExecButton()
    {
        Ray r = new Ray(controller.position, transform.position - controller.position);
        RaycastHit rh;

        //if the ray hit something
        if (Physics.Raycast(r, out rh, 10000))
        {
            pos = rh.point;
            MenuButton m = rh.collider.transform.GetComponent<MenuButton>(); //get the MenuButton of the object that was hit
            if (m != null) //if the object has a MenuButton
            {
                thisrenderer.material = green;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space) || (Input.GetKey(KeyCode.Space) && m.hold))
                {
                    //if it is possible to interact at the moment, or if the button canAlwaysInteract
                    if (RoomTimer.canInteract || m.canAlwaysInteract)
                    {
                        rh.collider.transform.GetComponent<MenuButton>().execButton();
                    }
                }
            }

        }
        else
        {
            thisrenderer.material = red;
        }
    }

    private void CheckAuth(bool b)
    {
        authenticated = b;
    }

    private void OnDestroy()
    {
        Events._Authenticated -= CheckAuth;
    }
}
