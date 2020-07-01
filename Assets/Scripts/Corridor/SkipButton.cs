using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MenuButton
{
    protected override void exec()
    {
        SceneManager.LoadScene("Room");
    }
}
