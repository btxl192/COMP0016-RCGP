using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//should be attached to the "handle" object of the built in scroll bars
public class MainMenuScrollBar : MenuButton
{
    [SerializeField]
    private float scrollspeed;

    Vector3 prevPos;
    Scrollbar thisScroll;

    protected override void Start()
    {
        thisScroll = transform.parent.parent.GetComponent<Scrollbar>();
        prevPos = ControllerPointer.pos;
        StartCoroutine(initcollider());
    }

    public override void exec()
    {
        if (ControllerPointer.pos.y > prevPos.y)
        {
            //scroll up
            thisScroll.value += scrollspeed;
        }
        else if (ControllerPointer.pos.y < prevPos.y)
        {
            //scroll down
            thisScroll.value -= scrollspeed;
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
