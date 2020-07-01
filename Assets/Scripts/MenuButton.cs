using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButton : MonoBehaviour
{

    //usually cannot interact in between rounds - setting to true ignores this.
    public bool canAlwaysInteract = false;
    //repeatedly execute while the button is held down?
    public bool hold = false;

    protected bool pressedBefore = false;

    protected virtual void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            throw new System.Exception("Object does not have a collider");
        }
        
    }

    public void execButton()
    {       
        exec();
        pressedBefore = true;
    }

    protected abstract void exec();
}
