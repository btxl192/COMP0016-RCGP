using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBox : MenuButton
{

    private TMPro.TextMeshPro speechbox;
    private string speechboxText { get { return speechbox.text; } set { speechbox.text = value; Events.PatientSpeak(value); Events.Transcribe("Patient: " + value + "\n\n"); } }
    private int currentLine = 0;
    private static string[] script = { "" };
    protected override void Start()
    {
        
        base.Start();
        speechbox = transform.parent.GetChild(1).GetComponent<TMPro.TextMeshPro>();
        resetScript();
    }

    public static void setScript(string[] newscript)
    {
        script = newscript;
    }

    public void resetScript()
    {
        speechboxText = script[0];
        currentLine = 0;
    }

    void nextLine()
    {
        if (currentLine < script.Length - 1)
        {
            speechboxText = script[++currentLine];
        }        
    }

    protected override void exec()
    {
        nextLine();

    }
}

