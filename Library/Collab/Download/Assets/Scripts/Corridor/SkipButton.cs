using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MenuButton
{
    public override void exec()
    {
        SceneManager.LoadScene("Room");
    }
}
