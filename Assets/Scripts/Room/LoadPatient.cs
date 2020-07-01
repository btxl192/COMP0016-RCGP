using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadPatient : MonoBehaviour
{
    public GameObject SpeechBox;

    //NOTE: there will always be a null patient at the end to make it easy to assign patients in the editor
    //e.g. even if there is 1 patient, list length will be 2
    public List<GameObject> Patients; 

    private int _patientindex;
    public int patientindex 
    { 
        get { return _patientindex; }
        set 
        { 
            if (value < Patients.Count - 1 && value >= 0) //check if index is in bounds
            {
                _patientindex = value; 
            } 
            else 
            {
                _patientindex = 0;
                Debug.LogWarning("Patient index out of bounds - setting to 0");
            } 
        }
    }

    private GameObject prevPatient;
    private GameObject prevSpeechBox;

    void Start()
    {
        Events._RoundStarted += loadNextPatient;
        Events._RoundEnded += DestroyPatient;
    }

    private void loadNextPatient()
    {
        DestroyPatient();

        //Instantiate patient and set the speech box's parent to the patient
        prevPatient = Instantiate(Patients[patientindex]);
        prevSpeechBox = Instantiate(SpeechBox);
    }

    private void DestroyPatient()
    {
        //destroy the previous patient and speechbox objects;
        if (prevPatient != null) { Destroy(prevPatient); }
        if (prevSpeechBox != null) { Destroy(prevSpeechBox); }
    }

    private void OnDestroy()
    {
        Events._RoundStarted -= loadNextPatient;
        Events._RoundEnded -= DestroyPatient;
    }
}
