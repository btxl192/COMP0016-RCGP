using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to go to the specified screen on the canvas
public class MainMenuGoToScreen : MenuButton
{
    public GameObject toGo;

    protected override void exec()
    {
        Transform root = transform.root;
        for (int i = 0; i < transform.root.childCount; i++)
        {
            root.GetChild(i).gameObject.SetActive(false);
        }
        toGo.SetActive(true);
    }
}
