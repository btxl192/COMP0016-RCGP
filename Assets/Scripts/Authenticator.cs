using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authenticator : MonoBehaviour
{

    private string _iamApikey = WatsonAPIDetails.SpeechToText_APIKey;

    private float _timeout = 5;
    private float _authDelay = 0.1f;
    private float _retries = 5;

    void Start()
    {
        Time.timeScale = 0;
        Runnable.Run(CheckService());
    }  

    IEnumerator CheckService()
    {
        IamAuthenticator authenticator = new IamAuthenticator(apikey: _iamApikey);      
        for (int i = 0; i < _retries; i++)
        {
            Events.WifiWarningText(string.Format("Trying to connect to IBM Watson... Try {0}/{1}", i + 1, _retries));
            

            float currentTimeout = 0;
            while (!authenticator.CanAuthenticate() && currentTimeout < _timeout)
            {
                Events.Authenticated(authenticator.CanAuthenticate());
                currentTimeout += _authDelay;
                yield return new WaitForSecondsRealtime(_authDelay);
            }
            Events.Authenticated(authenticator.CanAuthenticate());
            if (authenticator.CanAuthenticate())
            {
                Time.timeScale = 1;
                break;
            }
        }
        if (!authenticator.CanAuthenticate())
        {
            Events.WifiWarningText("Failed to connect to IBM Watson. Please check your wifi connection");
        }
        
    }
}
