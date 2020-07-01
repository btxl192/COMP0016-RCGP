using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeleteTranscript : MainMenuGoToScreen
{

    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    private bool confirm = false;

    protected override void exec()
    {
        if (!confirm)
        {
            confirm = true;
            text.text = "Press again to confirm";
        }
        else
        {            
            File.Delete(TranscriptButton.filepath);
            base.exec();
        }
        
    }

    private void OnEnable()
    {
        confirm = false;
        text.text = "Delete";
    }
}
