using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    private GameObject player;
    private GameObject controller;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GameObject.FindGameObjectWithTag("Controller");
    }

    void Update()
    {
        transform.LookAt(2 * transform.position - (player.transform.position + controller.transform.position) * 0.5f);
    }
}
