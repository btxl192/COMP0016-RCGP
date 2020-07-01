using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpeechDecider : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro speechbox;
    private string speechboxText 
    { 
        get 
        { 
            return speechbox.text; 
        } 
        set 
        { 
            speechbox.text = value; 
            Events.PatientSpeak(value); 
            //Events.Transcribe("Patient: " + value + "\n\n"); 
        } 
    }

    private void Awake()
    {
        Events._SetScript += setScript;
        Events._GPTranscribed += transcribe;
        Events._RoundStarted += transcribe;
    }

    private void Start()
    {
        setScript(ScriptLoader.loadedscript.script[0]);
    }

    public void setScript(string newscript)
    {
        print(newscript);
        speechboxText = newscript;
    }

    private void OnDestroy()
    {
        Events._SetScript -= setScript;
        Events._GPTranscribed -= transcribe;
        Events._RoundStarted -= transcribe;
    }

    private void transcribe()
    {
        Events.Transcribe("Patient: " + speechboxText + "\n\n");
    }
}

