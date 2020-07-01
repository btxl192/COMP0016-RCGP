using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDetailsTabManager : MonoBehaviour
{
    public GameObject tabObject;
    public Transform tabContainer;
    public GameObject notification;
    public GameObject patientdetailsglowy;
    public TMPro.TextMeshPro patientdetails;

    public static List<bool> tabschecked = new List<bool>();

    private static string summaryTabName;
    private static string summaryTabDetail;

    private static float spacing = 0.01f;
    private static GameObject tab;
    private static Transform tabContainerTransform;

    private void Start()
    {
        Events._RoundEnded += ClearTabs;
        Events._ScriptLoadedEvent += initTabs;
        if (tab == null || tabContainerTransform == null)
        {
            tab = tabObject;
            tabContainerTransform = tabContainer;
        }   
    }

    private static void SortTabs()
    {
        for (int i = 0; i < tabContainerTransform.childCount; i ++)
        {
            tabContainerTransform.GetChild(i).localPosition = new Vector3(0, (-0.1f - spacing) * i, 0);
           
        }
    }

    public static void InstantiateTab(string tabName, string tabDetail)
    {
        Instantiate(tab, tabContainerTransform).transform.GetChild(0).GetComponent<PatientDetailsTab>().init(tabName, tabDetail, tabschecked.Count);
        tabschecked.Add(false);
        SortTabs();
    }

    public static void SetSummary(string tabName, string tabDetail)
    {      
        summaryTabName = tabName;
        summaryTabDetail = tabDetail;
    }

    private void initTabs()
    {
        patientdetails.text = "Patient file";
        InstantiateTab(summaryTabName, summaryTabDetail);
    }

    private void ClearTabs()
    {
        tabschecked.Clear();
        foreach(Transform c in tabContainerTransform)
        {
            GameObject.Destroy(c.gameObject);
        }
    }

    private void OnDestroy()
    {
        Events._RoundEnded -= ClearTabs;
        Events._ScriptLoadedEvent -= initTabs;
    }

    private void Update()
    {
        bool allclicked = true;
        foreach (bool b in tabschecked)
        {
            allclicked = allclicked && b;
        }

        notification.SetActive(!allclicked);
        patientdetailsglowy.SetActive(!allclicked);

    }
}
