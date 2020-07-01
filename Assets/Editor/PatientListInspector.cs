using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadPatient), true)]
public class PatientListInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Drag a patient prefab on to the None field to add that patient");

        LoadPatient thisloadpatient = (LoadPatient)target;

        thisloadpatient.Patients.RemoveAll(n => n == null);
        thisloadpatient.Patients.Add(null);

    }
}