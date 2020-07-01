/**
* Copyright 2020 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
//This class was copied from ExampleTextToSpeechV1.cs, with modifications to API details and the addition of saying what the patient says
using IBM.Watson.TextToSpeech.V1;
using IBM.Watson.TextToSpeech.V1.Model;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Cloud.SDK;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField]
    private bool loggingenabled;


    private string iamApikey = WatsonAPIDetails.TextToSpeech_APIKey;
    private string serviceUrl = WatsonAPIDetails.TextToSpeech_ServiceURL;

    private TextToSpeechService service;
    private string femaleVoice = "en-GB_KateV3Voice";
    private string maleVoice = "en-US_HenryV3Voice";
    private string synthesizeText = "Hello, welcome to the Watson Unity SDK!";
    private string synthesizeMimeType = "audio/wav";

    private bool ready = false;

    #region PlayClip
    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            GameObject.Destroy(audioObject, clip.length);
        }
    }
    #endregion

    private void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
        Events._PatientSpeak += say;
    }

    private IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(iamApikey))
        {
            throw new IBMException("Please add IAM ApiKey to the Iam Apikey field in the inspector.");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

        while (!authenticator.CanAuthenticate())
        {
            yield return null;
        }

        service = new TextToSpeechService(authenticator);
        if (!string.IsNullOrEmpty(serviceUrl))
        {
            service.SetServiceUrl(serviceUrl);
        }

        //Runnable.Run(ExampleSynthesize());
        ready = true;
    }

    private void say(string s)
    {
        if (ready)
        {
            synthesizeText = s;
            Runnable.Run(ExampleSynthesize());
        }
    }

    #region Synthesize
    private IEnumerator ExampleSynthesize()
    {
        byte[] synthesizeResponse = null;
        AudioClip clip = null;
        service.Synthesize(
            callback: (DetailedResponse<byte[]> response, IBMError error) =>
            {
                synthesizeResponse = response.Result;
                if (loggingenabled) { Log.Debug("ExampleTextToSpeechV1", "Synthesize done!"); }
                clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                PlayClip(clip);
            },
            text: synthesizeText,
            voice: maleVoice,
            accept: synthesizeMimeType
        );

        while (synthesizeResponse == null)
            yield return null;

        yield return new WaitForSeconds(clip.length);
    }
    #endregion

    private void OnDestroy()
    {
        Events._PatientSpeak -= say;
    }
}

