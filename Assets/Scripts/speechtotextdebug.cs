using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speechtotextdebug : MonoBehaviour
{

    public TMPro.TextMeshProUGUI text;

    void Start()
    {
        Events._SpeechDetected += detected;
    }

    void detected(string s)
    {
        text.text = s;
    }

    private void OnDestroy()
    {
        Events._SpeechDetected -= detected;
    }
}
