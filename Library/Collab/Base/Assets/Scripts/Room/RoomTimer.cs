using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using TMPro;

public class RoomTimer : MonoBehaviour
{

    public float time;
    public float cooldownBegin;
    public float cooldownEnd;
    public int rounds;

    public GameObject timesup;
    public GameObject scenarios;

    public static bool canInteract
    {
        get;
        private set;
    }

    private TextMeshPro thistext;
    private TextMeshProUGUI timesuptext;
    private float currentCooldownBegin;
    private float currentCooldownEnd = 0;
    private float currentTime;
    private int currentRound = 0;
    private bool roundendedcalled = false;
    private bool preroundstartedcalled = false;

    //public static bool isScenarioPicked { get; private set; }

    //change logic in the update() here to use isRoundStarted?
    public static bool isRoundStarted { get; private set; }

    public delegate void RoundEvent();
    public static event RoundEvent RoundStarted;
    public static event RoundEvent PreRoundStarted;
    public static event RoundEvent RoundEnded;

    void Start()
    {
        MainMenuScenarioButton.buttonClicked += scenarioPicked;
        thistext = transform.GetChild(0).GetComponent<TextMeshPro>();
        timesuptext = timesup.GetComponent<TextMeshProUGUI>();
        currentTime = 0;
        currentCooldownBegin = cooldownBegin;
        //currentCooldownEnd = cooldownEnd;
        canInteract = true;
        //isScenarioPicked = true;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            thistext.text = FormatTime(currentTime);
        }
        else
        {
            if (currentCooldownEnd > 0 && currentRound < rounds)//if rounds have not finished yet
            {
                if (!roundendedcalled)
                {
                    RoundEnded?.Invoke();
                    roundendedcalled = true;
                    isRoundStarted = false;
                }
                canInteract = false;
                timesup.SetActive(true);
                if (currentRound > 0)// && !isScenarioPicked)
                {
                        
                    scenarios.SetActive(true);
                }

                currentCooldownEnd -= Time.deltaTime;
                if (currentRound == 0)
                {
                    //timesuptext.text = string.Format("Consultation starts in {0}...", (int)(currentCooldownEnd));
                }
                else
                {
                    timesuptext.text = string.Format("Time's up! Pick a new scenario (or stick with the previous) {0}...", (int)(currentCooldownEnd));
                }
 
                
                //isScenarioPicked = false;
                
            }
            else //when the round starts
            {
                if (currentCooldownBegin > 0)
                {
                    isRoundStarted = true;
                    if (!preroundstartedcalled)
                    {
                        PreRoundStarted?.Invoke();
                        preroundstartedcalled = true;
                    }
                    scenarios.SetActive(false);
                    timesup.SetActive(true);
                    currentCooldownBegin -= Time.deltaTime;
                    timesuptext.text = string.Format("Consultation starts in {0}...\nNext patient's summary\nhas been loaded", (int)(currentCooldownBegin));
                }
                else
                {
                    preroundstartedcalled = false;
                    RoundStarted?.Invoke();                   
                    roundendedcalled = false;
                    canInteract = true;
                    currentCooldownBegin = cooldownBegin;
                    currentCooldownEnd = cooldownEnd;
                    currentTime = time;
                    currentRound += 1;
                    timesup.SetActive(false);
                    scenarios.SetActive(false);
                }
            }
        }
        if (currentRound > rounds)
        {
            timesup.GetComponent<TextMeshProUGUI>().text = "END";
            timesup.SetActive(true);
            Destroy(this);
        }
    }

    string FormatTime(float t)
    {
        string seconds = ((int)(t % 60)).ToString();
        if (seconds.Length < 2)
        {
            seconds = "0" + seconds;
        }
        return ((int)(t / 60)).ToString() + ":" + seconds;
    }

    void scenarioPicked()
    {
        //isScenarioPicked = true;
    }

}