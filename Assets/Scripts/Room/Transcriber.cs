using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Transcriber : MonoBehaviour
{
    private string filename = "";
    private string filepath { get { return Application.persistentDataPath + "/" + filename.Replace("\\", "-").Replace("/", "-").Replace(":", "-") + ".txt"; } }
    private string currentLine = "";
    private string allText = "";
    private float maxdelay = 1;
    private float currentdelay = 0;
    private bool roundinprogress = true;

    private void Start()
    {
        Events._SpeechDetectedFinal += listen;
        Events._RoundEnded += endofround;
        Events._Transcribe += transcribe;
        Events._ScriptLoadedEvent += initFile;
        Events._RoundStarted += startofround;
    }

    private void initFile()
    {
        filename = generateFileName();
        Events.Transcribe(filename + "\n\n");
    }

    private void Update()
    {
        if (roundinprogress)
        {
            //transcribe if maxdelay has elapsed
            if (currentdelay < maxdelay)
            {
                currentdelay += Time.deltaTime;
            }
            else if (currentLine != "")
            {
                writeGP();
            }
        }

    }

    private void listen(string s)
    {
        //print(s + " [stt]"); //debug
        //check if the string is a continuation of the previous one
        //e.g. if it recognises "there is a" and then "there is a cat", it will use "there is a cat"
        if (!(currentLine.Equals("") || (s.Length >= currentLine.Length && s.Substring(0, currentLine.Length).Equals(currentLine))))
        {
            writeGP();
        }
        currentLine = s;
    }

    private void endofround()
    {
        roundinprogress = false;
        writeGP();
        //print(allText);
        writeToFile();
    }

    private void startofround()
    {
        roundinprogress = true;
        allText = "";
        currentLine = "";
        currentdelay = 0;
        initFile();
    }

    private static string generateFileName()
    {
        string now = "[" + DateTime.Now.ToString() + "]";
        return now + ScriptLoader.scripts[ScriptLoader.scriptIndex];
    }

    private void writeToFile()
    {
        File.WriteAllText(filepath, allText);
        print("file written to " + filepath); //debug
    }

    private void transcribe(string text)
    {
        allText += text;
    }

    private void writeGP()
    {       
        Events.Transcribe(string.Format("{0}: {1}\n\n", "GP", currentLine));
        Events.GPTranscribed();
        currentdelay = 0;
        currentLine = "";
    }
    private void OnDestroy()
    {
        Events._SpeechDetectedFinal -= listen;
        Events._RoundEnded -= endofround;
        Events._Transcribe -= transcribe;
        Events._ScriptLoadedEvent -= initFile;
        Events._RoundStarted -= startofround;
    }
}
