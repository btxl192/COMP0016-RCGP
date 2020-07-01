using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptButton : MainMenuGoToScreen
{

    private TMPro.TextMeshProUGUI textfield;

    private string mytext;

    public override void exec()
    {       
        textfield.text = mytext;
        base.exec();
    }

    public void settext(TMPro.TextMeshProUGUI textfield, string s)
    {
        this.textfield = textfield;
        mytext = s;
    }

    public void settogo(GameObject g)
    {
        toGo = g;
    }
}
