using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class TravelEvent : MonoBehaviour
{
    bool isTraveling;

    public TMP_Text promptText;
    public TMP_Text statsText;
    public TMP_Text updateText;

    public GameObject updateWindow;

    bool repeat;
    bool exit;
    static bool eActive;
    static bool startOnActive = false;
    public static bool complete;

    string[] negEvents = { "NgE1", "NgE2", "NgE3", "NgE4", "NgE5" };
    string[] neuEvents = { "NuE1", "NuE2", "NuE3", "NuE4", "NuE5" };
    string[] posEvents = { "PE1", "PE2", "PE3", "PE4", "PE5" };
    static string[] currEList;
    static int currENum;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("I start!");
        print(eActive);

        StatsManager.updateTravelerObjects();
        StatsManager.activateTravelers();
        setStats();
        
        promptText.text = "Press <color=#00cbff>SPACE</color> to continue.";
        isTraveling = false;

        updateWindow.SetActive(false);
        updateText.text = "";

        //isDayActive();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("we are updating wohoo!");
        if (Input.GetKeyDown("space") || startOnActive)
        {
            startOnActive = false;
            isTraveling = !isTraveling;

            if (isTraveling)
            {
                promptText.text = "Press <color=#00cbff>SPACE</color> to stop.";

                StartCoroutine(startDayCycle());
            }
            else
            {
                UnityEngine.Debug.Log("eActive = " + eActive);
                if (eActive)
                {
                    startOnActive = true;
                    print("destroying event before leaving...");
                    destroyUpdate();
                }

                SceneManager.LoadScene(0);
            }
        }
        else if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            exit = true;
            UnityEngine.Debug.Log("exit == " + exit);
        }

    }

    IEnumerator startDayCycle()
    {
        while (true)
        {
            UnityEngine.Debug.Log("------------PASS DAY-------------");
            repeat = false;

            startParty(); //Party walks
            startDay();
            yield return new WaitUntil(() => repeat == true);
        }
    }

    void startDay()
    {
        print("in pass day!");

        if (eActive) //If event was active
        {
            startEncounter(); //automatically enter encounter
        }
        else
        {
            Invoke("startEncounter", 1); //if else, wait
        }

        
        Invoke("stopParty", 2);
        Invoke("updateStats", 2);
        Invoke("setStats", 2);
        Invoke("resetLoop", 4);

        UnityEngine.Debug.Log("day passed!");
    }

    void setStats()
    {
        statsText.text = "Day <color=#ff0083>" + StatsManager.day + "</color> of <color=#ff0083>" + StatsManager.seasonsSet[StatsManager.seasonNum] + "</color>\nWeather: <color=#ff0083>" + StatsManager.weather + "</color>\nHealth: <color=#ff0083>" + StatsManager.health + "</color>\nFood: <color=#ff0083>" + StatsManager.food + "</color> portions\nNext Wayfinder: <color=#ff0083>" + StatsManager.nextWay + "</color> leagues";
    }

    void updateStats()
    {
        UnityEngine.Debug.Log("in updateStats!");

        //yield return new WaitUntil(() => complete == true);

        StatsManager.updateDate();
        StatsManager.chooseWeather();
        //StatsManager.updateFood();
        StatsManager.updateDistance();
        StatsManager.updateHealth();
    }

    void startParty()
    {
        for (int i = 0; i < StatsManager.party.Length; i++)
        {
            StatsManager.party[i].GetComponent<WalkScript>().walkStart();
        }
    }

    void stopParty()
    {
        for (int i = 0; i < StatsManager.party.Length; i++)
        {
            StatsManager.party[i].GetComponent<WalkScript>().walkStop();
        }
    }

    void resetLoop()
    {
        eActive = false;
        complete = false;
        repeat = true;
    }

    public void startUpdate(string message)
    {
        StartCoroutine(createUpdate(message));
    }
    IEnumerator createUpdate(string message)
    {
        UnityEngine.Debug.Log("create function entered!");

        updateWindow.SetActive(true);
        updateText.text = message;
        eActive = true;
        exit = false;

        Time.timeScale = 0f;
        UnityEngine.Debug.Log("code still runs!");

        yield return new WaitUntil(() => exit == true);

        eActive = false;
        destroyUpdate();
    }

    void destroyUpdate()
    {
        Time.timeScale = 1f;
        UnityEngine.Debug.Log("destroy function entered!");
        //yield return new WaitUntil(() => exit == true);
        //UnityEngine.Debug.Log("wait time passed!");
        updateWindow.SetActive(false);
        updateText.text = "";
    }

    void startEncounter()
    {
        UnityEngine.Debug.Log("in encounter");

        if (eActive) //if event is previously active
        {
            //execute saved event
            executeEncounter(currEList, currENum);
        }
        else //if else, create a new random one
        {
            int percent = Random.Range(1, 100);
            int eNum = Random.Range(1, 5);

            UnityEngine.Debug.Log(percent + "%, #" + eNum);

            if (StatsManager.averageHP < 30) //POOR
            {
                UnityEngine.Debug.Log("in poor");
                if (percent <= 60)
                {
                    //NEGATIVE
                    executeEncounter(negEvents, eNum);
                }
                else if (percent <= 85)
                {
                    //NEUTRAL
                    executeEncounter(neuEvents, eNum);
                }
                else
                {
                    //POSITIVE
                    executeEncounter(posEvents, eNum);
                }
            }
            else if (StatsManager.averageHP <= 70) //FAIR
            {
                UnityEngine.Debug.Log("in fair");
                if (percent <= 25)
                {
                    //NEGATIVE
                    executeEncounter(negEvents, eNum);
                }
                else if (percent <= 75)
                {
                    //NEUTRAL
                    executeEncounter(neuEvents, eNum);
                }
                else
                {
                    //POSITIVE
                    executeEncounter(posEvents, eNum);
                }
            }
            else //GOOD
            {
                UnityEngine.Debug.Log("in good");
                if (percent <= 15)
                {
                    //NEGATIVE
                    executeEncounter(negEvents, eNum);
                }
                else if (percent <= 40)
                {
                    //NEUTRAL
                    executeEncounter(neuEvents, eNum);
                }
                else
                {
                    //POSITIVE
                    executeEncounter(posEvents, eNum);
                }
            }

        }
    }

    void executeEncounter(string[] list, int num)
    {
        UnityEngine.Debug.Log("in execute");
        currEList = list;
        currENum = num;
        StartCoroutine(createUpdate(list[num]));
    }

    
    void isDayActive()
    {
        UnityEngine.Debug.Log("checking encounter: eActive = " + eActive);
        if(eActive)
        {
            //executeEncounter(currEList, currENum);
            isTraveling = true;
        }
    }

    void print(string text)
    {
        UnityEngine.Debug.Log(text);
    }
    /*TO DO:
     * Fix problem w/ exiting an event - You need to check if enocuanter is runnig in start encounter and then load the old encounter good luuck!
     * Second check up on encounater breaks it
     * Add randomness to events
     * Fix multiple update stack up
     * 
     * The plan:
     * Ok so at start we will check if the day was active when we left it (aka there is an update running)
     * If so, we will start a new day BUUUT skip wait for walking (we still need to activate walking) and create an event
     * That was equal to the last event (oof)
     * If this event is exited from, we save the event, set eActive to true (singaling the event is still running), and destroy the 
     * current event to prevent bugs. If all goes according plan we shoullld be fine but idk... OK GO!!
     */
}
