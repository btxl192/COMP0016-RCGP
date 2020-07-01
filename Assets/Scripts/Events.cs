using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //define delegates here
    public delegate void VoidEvent();
    public delegate void StringEvent(string s);
    public delegate void BoolEvent(bool b);

    //define events here
    public static void MainMenuEnterButtonClicked() { _MainMenuEnterButtonClicked?.Invoke(); }
    public static event VoidEvent _MainMenuEnterButtonClicked;

    public static void MainMenuScenarioButtonClicked() { _MainMenuScenarioButtonClicked?.Invoke(); }
    public static event VoidEvent _MainMenuScenarioButtonClicked;

    public static void ScriptLoadedEvent() { _ScriptLoadedEvent?.Invoke(); }
    public static event VoidEvent _ScriptLoadedEvent;

    public static void PreRoundStarted() { _PreRoundStarted?.Invoke(); }
    public static event VoidEvent _PreRoundStarted;

    public static void RoundStarted() { _RoundStarted?.Invoke(); }
    public static event VoidEvent _RoundStarted;

    public static void RoundEnded() { _RoundEnded?.Invoke(); }
    public static event VoidEvent _RoundEnded;

    public static void SpeechDetected(string s) { _SpeechDetected?.Invoke(s); }
    public static event StringEvent _SpeechDetected; //called whenever speech is recognised in SpeechToText.cs, triggered for each alternative words recognised

    public static void SpeechDetectedFinal(string s) { _SpeechDetectedFinal?.Invoke(s); }
    public static event StringEvent _SpeechDetectedFinal; //called whenever speech is recognised in SpeechToText.cs, triggered for the final word recognised

    public static void Authenticated(bool b) { _Authenticated?.Invoke(b); }
    public static event BoolEvent _Authenticated;

    public static void WifiWarningText(string s) { _WifiWarningText?.Invoke(s); }
    public static event StringEvent _WifiWarningText;

    public static void Transcribe(string s) { _Transcribe?.Invoke(s); }
    public static event StringEvent _Transcribe;

    public static void PatientSpeak(string s) { _PatientSpeak?.Invoke(s); }
    public static event StringEvent _PatientSpeak;

    public static void SetScript(string s) { _SetScript?.Invoke(s); }
    public static event StringEvent _SetScript;

    public static void GPTranscribed() { _GPTranscribed?.Invoke(); }
    public static event VoidEvent _GPTranscribed;
}
