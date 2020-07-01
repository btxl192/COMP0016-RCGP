using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifiWarning : MonoBehaviour
{

    public GameObject wifiwarning;
    public TMPro.TextMeshProUGUI text;

    void Awake()
    {
        Events._Authenticated += CheckAuth;
        Events._WifiWarningText += changetext;
    }

    private void CheckAuth(bool b)
    {
        wifiwarning.SetActive(!b);
    }

    private void changetext(string s)
    {
        text.text = s;
    }

    private void OnDestroy()
    {
        Events._Authenticated -= CheckAuth;
        Events._WifiWarningText -= changetext;
    }
}
