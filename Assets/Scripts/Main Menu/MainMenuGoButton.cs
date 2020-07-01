using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGoButton : MenuButton
{
    public GameObject loading;
    protected override void exec()
    {
        loading.SetActive(true);
        SceneManager.LoadScene("Corridor");
    }
}
