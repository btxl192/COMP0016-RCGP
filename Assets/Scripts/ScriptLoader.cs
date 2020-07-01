using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class ScriptInfo
{
    public int patientIndex;
    public string[] script;
    public string[] tabNames;
    public string[] tabDetails;
    public string[] keywords;

    public ScriptInfo(int patientindex, string[] script, string[] tabNames, string[] tabDetails, string[] keywords)
    {
        this.patientIndex = patientindex;
        this.script = script;
        this.tabNames = tabNames;
        this.tabDetails = tabDetails;
        this.keywords = keywords;
    }
}

public class ScriptLoader : MonoBehaviour
{
    public static List<string> scripts;
    public static int scriptIndex = 0;

    public LoadPatient patientloader;

    private ScriptInfo undefinedscript = new ScriptInfo(0, new string[] { "undefined" }, new string[] { "undefined summary" }, new string[] { "undefined details" }, new string[]  { "undefined words"});
    public static ScriptInfo loadedscript { get; private set; }
    private bool[] loadedTabIndexes;

    public void Start()
    {
        Events._PreRoundStarted += NextScript;
        Events._SpeechDetected += ListenForTab;
        Events._SpeechDetectedFinal += ListenForKeyword;
    }

    void NextScript()
    {
        if (scripts == null || scripts.Count <= 0)
        {
            scripts = new List<string>();
            foreach (TextAsset f in Resources.LoadAll<TextAsset>("Content"))
            {
                if (loadscript(f.name))
                {
                    scripts.Add(f.name);
                }              
            }
            scriptIndex = scripts.Count - 1;            
        }
        if (scripts.Count == 0)
        {
            loadedscript = undefinedscript;
        }
        else
        {
            loadedscript = JsonUtility.FromJson<ScriptInfo>(Resources.Load("Content/" + scripts[scriptIndex]).ToString());
        }
        

        patientloader.patientindex = loadedscript.patientIndex;

        PatientDetailsTabManager.SetSummary(loadedscript.tabNames[0], loadedscript.tabDetails[0]);
        loadedTabIndexes = new bool[loadedscript.tabNames.Length];
        loadedTabIndexes[0] = true;       
        Events.ScriptLoadedEvent();
    }

    private bool loadscript(string loc)
    {
        try
        {
            loadedscript = JsonUtility.FromJson<ScriptInfo>(Resources.Load("Content/" + loc).ToString());
            return loadedscript.tabNames.Length > 0 && loadedscript.tabDetails.Length > 0;
        }
        catch
        {
            return false;
        }
    }
    void ListenForKeyword(string s)
    {
        string[] keys= loadedscript.keywords;
        string[] script= loadedscript.script;
        s = s.ToLower().Trim();
        string[] split = s.Split(' ');

        bool keywordFound = false;
        foreach (string word in split)
        {
            if (word.Length > 2)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].Contains(word))
                    {
                        Events.SetScript(script[i]);
                        keywordFound = true;
                        break;
                    }
                }
                if (keywordFound)
                {
                    break;
                }
            }
        }
        if (!keywordFound)
        {
            Events.SetScript("Can you repeat that?");
        }
    }
  
    void ListenForTab(string s)
    {
        //if the string passed into this function is a tab name
        for (int i = 0; i < loadedscript.tabNames.Length; i++)
        {
            string tabname = loadedscript.tabNames[i].ToLower().Trim();
            s = s.ToLower().Trim();
          
            if (!loadedTabIndexes[i] && (tabname.Contains(s) || tabname.Equals(s) || s.Contains(tabname)))
            {
                PatientDetailsTabManager.InstantiateTab(tabname, loadedscript.tabDetails[i]);
                loadedTabIndexes[i] = true;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        Events._PreRoundStarted -= NextScript;
        Events._SpeechDetected -= ListenForTab;
        Events._SpeechDetectedFinal -= ListenForKeyword;
    }
}
