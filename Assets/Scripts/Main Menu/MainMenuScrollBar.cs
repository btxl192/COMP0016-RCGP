using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//should be attached to the "handle" object of the built in scroll bars
public class MainMenuScrollBar : MenuButton
{

    private float scrollspeed = 0.2f;

    Vector3 prevPos;
    Scrollbar thisScroll;

    protected override void Start()
    {
        thisScroll = transform.parent.parent.GetComponent<Scrollbar>();
        prevPos = ControllerPointer.pos;
        
    }

    private void OnEnable()
    {
        StartCoroutine(initcollider());
    }

    protected override void exec()
    {
        float difference = ControllerPointer.pos.y - transform.position.y;
        if (difference > 0)
        {
            //scroll up
            thisScroll.value += scrollspeed * Mathf.Abs(difference);
        }
        else if (difference < 0)
        {
            //scroll down
            thisScroll.value -= scrollspeed * Mathf.Abs(difference);
        }
        prevPos = ControllerPointer.pos;
    }

    private IEnumerator initcollider()
    {
        //wait for a bit in case the rect transform hasn't resized yet
        yield return new WaitForSeconds(0.1f);
        Rect handlerect = transform.GetComponent<RectTransform>().rect;
        GetComponent<BoxCollider>().size = new Vector3(handlerect.width, handlerect.height, 0);

    }
}
