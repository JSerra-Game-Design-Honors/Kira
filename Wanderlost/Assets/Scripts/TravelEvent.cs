using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class TravelEvent : MonoBehaviour
{
    bool isTraveling;

    public TMP_Text promptText;
    public TMP_Text statsText;
    public TMP_Text updateText;

    public GameObject updateWindow;

    bool repeat;
    public bool exit; //exit event
    bool exitScene;
    public static bool complete;
    bool arrived = false;

    string[] negEvents = { "NgE1", "NgE2", "NgE3", "NgE4", "NgE5" };
    string[] neuEvents = { "NuE1", "NuE2", "NuE3", "NuE4", "NuE5" };
    string[] posEvents = { "PE1", "PE2", "PE3", "PE4", "PE5" };

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("I start!");
        print(MapManager.currLoc);

        StatsManager.updateTravelerObjects();
        StatsManager.activateTravelers();
        setStats();
        
        promptText.text = "Press <color=#00cbff>SPACE</color> to continue.";
        isTraveling = false;

        updateWindow.SetActive(false);
        updateText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && exitScene && !arrived)
        {
            print(Input.GetKeyDown("space") && exitScene);

            isTraveling = !isTraveling;

            if (isTraveling)
            {
                StartCoroutine(startDayCycle());
            }
            else
            {
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
        exitScene = false;
        promptText.text = "";

        print("in pass day!");

        Invoke("startEncounter", 1);
        Invoke("updateStats", 2);
        Invoke("stopParty", 2);
        Invoke("setStats", 3);
        Invoke("resetLoop", 4);

        UnityEngine.Debug.Log("day passed!");
    }

    public void updateStats()
    {
        UnityEngine.Debug.Log("in updateStats!");

        StatsManager.updateDate();
        StatsManager.chooseWeather();
        MapManager.updateDistance();
        startUpdateHealth();
    }

    void setStats()
    {
        print("--Here we are in set stats.");
        statsText.text = "Day <color=#ff0083>" + StatsManager.day + "</color> of <color=#ff0083>" + StatsManager.seasonsSet[StatsManager.seasonNum] + "\n" + MapManager.currDist + "</color> miles to <color=#ff0083>" + MapManager.map[MapManager.currLoc].name + "</color>\nWeather: <color=#ff0083>" + StatsManager.weather + "</color>\nHealth: <color=#ff0083>" + StatsManager.health + "</color>\nFood: <color=#ff0083>" + StatsManager.food + "</color> portions";//nNext Wayfinder: <color=#ff0083>" + StatsManager.nextWay + "</color> leagues";

        //give player the chance to exit
        if (!arrived)
        {
            promptText.text = "Press <color=#00cbff>SPACE</color> to stop.";
        }
        exitScene = true;
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
        complete = false;
        repeat = true;
    }

    public void startUpdate(string message)
    {
        StartCoroutine(createUpdate(message));
    }

    IEnumerator createUpdate(string message)
    {
        print("hi");
        UnityEngine.Debug.Log("--create function entered!");

        updateWindow.SetActive(true);
        updateText.text = message;
        exit = false;

        Time.timeScale = 0f;
        UnityEngine.Debug.Log("code still runs!");

        yield return new WaitUntil(() => exit == true);

        destroyUpdate();

        if (arrived)
        {
            MapManager.updateLocation();
            print(MapManager.currLoc);
            arrived = false;

            print(MapManager.currLoc + " == " + MapManager.map.Length);
            if (MapManager.currLoc == MapManager.map.Length)
            {
                print("hello");
                SceneManager.LoadScene(5);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
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

        int encounterChance = UnityEngine.Random.Range(1, 100);

        if (encounterChance <= 25)
        {
            int percent = UnityEngine.Random.Range(1, 100);
            int eNum = UnityEngine.Random.Range(1, 5);

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
        StartCoroutine(createUpdate(list[num]));
    }

    public void startUpdateHealth()
    {
        StartCoroutine(updateHealth());
    }

    public IEnumerator updateHealth()
    {
        int totalHP = 0;
        int aliveTemp = 0;

        for (int i = 0; i < StatsManager.healthNum.Length; i++)
        {
            if (StatsManager.activeParty[i])
            {
                StatsManager.healthNum[i] += StatsManager.calculateGenPenalty();

                print(StatsManager.healthNum[i]);

                if (StatsManager.healthNum[i] <= 0)
                {
                    StatsManager.playerDeath(i);
                    yield return new WaitUntil(() => exit == true);
                }
                else
                {
                    if (StatsManager.healthNum[i] > 100)
                    {
                        StatsManager.healthNum[i] = 100;
                    }
                    totalHP += StatsManager.healthNum[i];
                    aliveTemp++;
                }
            }
        }

        if (aliveTemp > 0)
        {
            StatsManager.alive = aliveTemp;
            StatsManager.averageHP = totalHP / StatsManager.alive;
            print(StatsManager.averageHP);

            if (StatsManager.averageHP < 30)
            {
                StatsManager.health = "Poor";
            }
            else if (StatsManager.averageHP <= 70)
            {
                StatsManager.health = "Fair";
            }
            else
            {
                StatsManager.health = "Good";
            }
            complete = true;
        }
        else
        {
            StatsManager.gameOver();
        }
    }

    public void arrivedAtWF()
    {
        print("IN ARRIVED");
        startUpdate("You have arrived at <color=#ff0083>" + MapManager.map[MapManager.currLoc].name+"</color>.");
        arrived = true;
    }

    void print(string text)
    {
        UnityEngine.Debug.Log(text);
    }
    /*TO DO:
     * 
     * 
     */
}
