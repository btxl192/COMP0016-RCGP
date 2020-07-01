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

    protected override void Start()
    {
        base.Start();
        exec();
    }

    public override void exec()
    {
        clear();
        foreach(string s in Directory.GetFiles(Application.persistentDataPath))
        {
            if (s.Substring(s.Length - 4).Equals(".txt"))
            {
                GameObject g = Instantiate(transcriptbutton, content);
                //set text of the button
                g.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = s;
                g.GetComponent<TranscriptButton>().settext(transcriptdisplaytextfield, File.ReadAllText(s));
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
