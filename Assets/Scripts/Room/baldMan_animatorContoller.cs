using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baldMan_animatorContoller : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    //public bool clickedAnimator=false;
    void Start()
    {
        anim = GetComponent<Animator>();
        Events._PatientSpeak += patientspeak;
    }

    void patientspeak(string s)
    {
        StartCoroutine(animate());
    }

    private void OnDestroy()
    {
        Events._PatientSpeak -= patientspeak;
    }

    IEnumerator animate()
    {
        anim.SetBool("talk", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("talk", false);
    }
}
