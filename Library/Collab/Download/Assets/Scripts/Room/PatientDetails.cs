using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDetails : MenuButton
{

    public GameObject patientdetails;

    protected override void Start()
    {
        base.Start();
        canAlwaysInteract = true;
        Events._RoundEnded += disable;
    }

    public override void exec()
    {
        if (RoomTimer.isRoundStarted)
        {
            patientdetails.SetActive(!patientdetails.activeSelf);
        }
    }

    void disable()
    {
        patientdetails.SetActive(false);
    }

    private void OnDestroy()
    {
        Events._RoundEnded -= disable;
    }
}
