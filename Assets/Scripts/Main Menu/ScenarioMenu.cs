using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMenu : MonoBehaviour
{

    public GameObject scenariobutton;
    public Transform content;
    void Start()
    {
        reload();
        Events._MainMenuEnterButtonClicked += reload;
    }

    public void reload()
    {
        ScriptLoader.scripts = new List<string>();
        foreach (TextAsset f in Resources.LoadAll<TextAsset>("Content"))
        {
            GameObject g = Instantiate(scenariobutton, content);
            //set text of the button
            g.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = f.name;
            ScriptLoader.scripts.Add(f.name);
        }
    }

    private void OnDestroy()
    {
        Events._MainMenuEnterButtonClicked -= reload;
    }
}
