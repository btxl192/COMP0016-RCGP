using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScenarioBackButton : MainMenuGoToScreen
{
    public Transform content;
    public override void exec()
    {
        base.exec();
        ScriptLoader.scriptIndex = 0;
        //Clear the content
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }
    }
}
