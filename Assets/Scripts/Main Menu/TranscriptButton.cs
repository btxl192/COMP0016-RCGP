using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptButton : MainMenuGoToScreen
{

    private TMPro.TextMeshProUGUI textfield;
    private string mytext;
    private string _filepath;

    public static string filepath { get; private set; }


    protected override void exec()
    {       
        textfield.text = mytext;
        filepath = _filepath;
        base.exec();
    }

    public void init(TMPro.TextMeshProUGUI textfield, string s, string path)
    {
        this.textfield = textfield;
        mytext = s;
        _filepath = path;
    }

    public void settogo(GameObject g)
    {
        toGo = g;
    }
}
