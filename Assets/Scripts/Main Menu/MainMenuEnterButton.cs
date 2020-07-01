using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEnterButton : MainMenuGoToScreen
{
    protected override void exec()
    {
        base.exec();
        Events.MainMenuEnterButtonClicked();
    }
}
