﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MenuButton
{
    protected override void exec()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
