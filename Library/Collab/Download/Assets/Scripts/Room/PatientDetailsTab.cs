using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientDetailsTab : MenuButton
{

    public TextMeshPro tabNameObject;
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

    private TextMeshPro detailsText
    {
        get { return transform.parent.parent.parent.GetChild(0).GetComponent<TextMeshPro>(); }
    }

    public override void exec()
    {
        detailsText.text = details;
    }

    public void init(string tabName, string tabDetails)
    {
        this.tabName = tabName;
        details = tabDetails;
        
    }
}
