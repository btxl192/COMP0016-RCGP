using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientDetailsTab : MenuButton
{

    public TextMeshPro tabNameObject;
    public GameObject glowy;

    public int tabindex = -1;

    public string details
    {
        get; set;
    }

    private string _tabName;
    public string tabName
    {
        get { return _tabName; }
        set { _tabName = value; tabNameObject.text = _tabName; }
    }

    private TextMeshPro detailsText;

    protected override void Start()
    {
        base.Start();
        detailsText = transform.root.GetChild(1).GetChild(1).GetComponent<TextMeshPro>();
    }

    protected override void exec()
    {
        if (!pressedBefore)
        {
            glowy.SetActive(false);
            PatientDetailsTabManager.tabschecked[tabindex] = true;
            pressedBefore = true;
        }
        detailsText.text = details;
    }

    public void init(string tabName, string tabDetails, int index)
    {
        this.tabName = tabName;
        details = tabDetails;
        tabindex = index;
        
    }

}
