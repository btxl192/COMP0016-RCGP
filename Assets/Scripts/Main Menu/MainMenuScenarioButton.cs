using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScenarioButton : MenuButton
{
    private string originalText;
    private TMPro.TextMeshProUGUI textobj;

    protected override void Start()
    {
        base.Start();
        textobj = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        originalText = textobj.text;
        Events._MainMenuScenarioButtonClicked += handleUpdateText;
        handleUpdateText();
    }
    protected override void exec()
    {
        ScriptLoader.scriptIndex = transform.GetSiblingIndex();
        Events.MainMenuScenarioButtonClicked();
    }

    private void handleUpdateText()
    {
        if (ScriptLoader.scriptIndex == transform.GetSiblingIndex())
        {
            textobj.text = originalText + " (SELECTED)";
        }
        else
        {
            textobj.text = originalText;
        }
    }

    private void OnDestroy()
    {
        Events._MainMenuScenarioButtonClicked -= handleUpdateText;
    }
}
