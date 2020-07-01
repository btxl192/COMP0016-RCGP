using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*public class CorridorSceneManager : MonoBehaviour
{

    public float vidTime;
    public GameObject loadingtext;
    public UnityEngine.Video.VideoClip corrdor;

    private float currentTime = 0;
    private void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
    }
    void Update()
    {
        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        if (!vp.isPlaying)
        {
             loadingtext.SetActive(true);
             SceneManager.LoadScene("Room");
        }
        
    }
}*/

 
     public class CorridorSceneManager : MonoBehaviour
{

    public float vidTime;
    public GameObject loadingtext;
   

    private float currentTime = 0;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= vidTime)
        {   
            //this makes video not skip to next scene
            //loadingtext.SetActive(true);
            SceneManager.LoadScene("Room");
        }
    }
}
