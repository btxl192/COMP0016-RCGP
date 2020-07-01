using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RefreshTranscripts : MenuButton
{
    [SerializeField]
    private GameObject transcriptbutton;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private TMPro.TextMeshProUGUI transcriptdisplaytextfield;

    [SerializeField]
    private GameObject transcriptdisplay;

    private void OnEnable()
    {
        exec();
    }

    protected override void exec()
    {
        clear();
        foreach(string s in Directory.GetFiles(Application.persistentDataPath))
        {
            if (s.Substring(s.Length - 4).Equals(".txt"))
            {
                GameObject g = Instantiate(transcriptbutton, content);
                //set text of the button
                string filename = s.Split('/')[s.Split('/').Length - 1];
                string date = filename.Split('[')[1].Split(']')[0];
                string scenario = filename.Split(']')[1].Split('.')[0];
                g.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = date + "\nScenario: " + scenario; 
                g.GetComponent<TranscriptButton>().init(transcriptdisplaytextfield, File.ReadAllText(s), s);
                g.GetComponent<TranscriptButton>().settogo(transcriptdisplay);
            }
        }
    }

    private void clear()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
