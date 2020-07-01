using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using IBM.Watson.SpeechToText.V1;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.DataTypes;

public class SpeechToText : MonoBehaviour
{
    [Tooltip("The service URL (optional). This defaults to \"https://stream.watsonplatform.net/speech-to-text/api\"")]
    [SerializeField]
    private string _serviceUrl;
    [Tooltip("Text field to display the results of streaming.")]
    public Text ResultsField;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string _iamApikey;
    private SpeechToTextService _service;
    private AudioClip recording;

    private void Start()
    {
        
        
        recording = Microphone.Start(null, true, 100, 44000);
        Runnable.Run(CreateService());
        //AudioSource aud = GetComponent<AudioSource>();

        //aud.clip = recording;
        //aud.Play();
    }
    private void OnError(string error)
    {

        Log.Debug("ExampleStreaming.OnError()", "Error! {0}", error);
    }

    private IEnumerator CreateService()
    {
        IamAuthenticator authenticator = new IamAuthenticator(apikey: _iamApikey);

        //  Wait for tokendata
        while (!authenticator.CanAuthenticate())
            yield return null;

        _service = new SpeechToTextService(authenticator);
        _service.DetectSilence = true;
        _service.EnableWordConfidence = true;
        _service.EnableTimestamps = true;
        _service.SilenceThreshold = 0.01f;
        _service.MaxAlternatives = 1;
        _service.EnableInterimResults = true;
        _service.OnError = OnError;
        _service.InactivityTimeout = -1;
        _service.ProfanityFilter = false;
        _service.SmartFormatting = true;
        _service.SpeakerLabels = false;
        _service.WordAlternativesThreshold = null;
        _service.EndOfPhraseSilenceTime = null;
        _service.StartListening(OnRecognize, OnRecognizeSpeaker);

        /*
        while(true)
        {
            print("a");
            AudioData record = new AudioData();
            record.MaxLevel = 1000000;// Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
            record.Clip = recording;// AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                                    //record.Clip.SetData(samples, 0);

            _service.OnListen(record);
            yield return new WaitForSeconds(2f);
        }*/

        Log.Debug("ExampleStreaming.RecordingHandler()", "devices: {0}", Microphone.devices);
        //_recording = Microphone.Start(_microphoneID, true, _recordingBufferSize, _recordingHZ);
        yield return null;      // let _recordingRoutine get set..
        /*
        if (_recording == null)
        {
            StopRecording();
            yield break;
        }
        */
        bool bFirstBlock = true;
        int midPoint = recording.samples / 2;
        float[] samples = null;

        while (recording != null)
        {
            int writePos = Microphone.GetPosition(null);
            if (writePos > recording.samples || !Microphone.IsRecording(null))
            {
                Log.Error("ExampleStreaming.RecordingHandler()", "Microphone disconnected.");

                //StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
              || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, recording.channels, 44000, false);
                record.Clip.SetData(samples, 0);

                _service.OnListen(record);

                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio, 
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)44000;

                yield return new WaitForSeconds(timeRemaining);
            }
        }
        yield break;

    }

    private void OnRecognize(SpeechRecognitionEvent result)
    {
        print("r");
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    string text = string.Format("{0} ({1}, {2:0.00})\n", alt.transcript, res.final ? "Final" : "Interim", alt.confidence);
                    Log.Debug("ExampleStreaming.OnRecognize()", text);
                    ResultsField.text = text;
                }

                if (res.keywords_result != null && res.keywords_result.keyword != null)
                {
                    foreach (var keyword in res.keywords_result.keyword)
                    {
                        Log.Debug("ExampleStreaming.OnRecognize()", "keyword: {0}, confidence: {1}, start time: {2}, end time: {3}", keyword.normalized_text, keyword.confidence, keyword.start_time, keyword.end_time);
                    }
                }

                if (res.word_alternatives != null)
                {
                    foreach (var wordAlternative in res.word_alternatives)
                    {
                        Log.Debug("ExampleStreaming.OnRecognize()", "Word alternatives found. Start time: {0} | EndTime: {1}", wordAlternative.start_time, wordAlternative.end_time);
                        foreach (var alternative in wordAlternative.alternatives)
                            Log.Debug("ExampleStreaming.OnRecognize()", "\t word: {0} | confidence: {1}", alternative.word, alternative.confidence);
                    }
                }
            }
        }
    }

    private void OnRecognizeSpeaker(SpeakerRecognitionEvent result)
    {
        if (result != null)
        {
            foreach (SpeakerLabelsResult labelResult in result.speaker_labels)
            {
                Log.Debug("ExampleStreaming.OnRecognizeSpeaker()", string.Format("speaker result: {0} | confidence: {3} | from: {1} | to: {2}", labelResult.speaker, labelResult.from, labelResult.to, labelResult.confidence));
            }
        }
    }
}
