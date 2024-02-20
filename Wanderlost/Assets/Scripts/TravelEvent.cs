using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class TravelEvent : MonoBehaviour
{

    public GameObject one, two, three, four, five;

    public GameObject[] party = new GameObject[5];
    public bool[] activeParty = { true, true, true, true, true };
    bool isTraveling;

    public TMP_Text promptText;
    public TMP_Text statsText;
    public TMP_Text updateText;

    public GameObject updateWindow;

    bool repeat;
    bool exit;
    public static bool complete;


    // Start is called before the first frame update
    void Start()
    {
        createParty();
        setStats();
        promptText.text = "Press <color=#00cbff>SPACE</color> to continue.";
        isTraveling = false;

        updateWindow.SetActive(false);
        updateText.text = "";

        UnityEngine.Debug.Log("about to create");
        //StartCoroutine(createUpdate("Hi."));
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("we are updating wohoo!");
        if (Input.GetKeyDown("space"))
        {
            isTraveling = !isTraveling;
            //exit = true;
        
        if (isTraveling)
        {
            promptText.text = "Press <color=#00cbff>SPACE</color> to stop.";

            StartCoroutine(startDayCycle());
            //Debug.Log("space on");
        }
        else
        {
            //promptText.text = "Press SPACE to continue.";
            SceneManager.LoadScene(0);
            //Debug.Log("space off");
        }
        }
        else if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            exit = true;
            UnityEngine.Debug.Log("exit == "+exit);
        }

    }

    void Awake()
    {
        UnityEngine.Debug.Log("I'm Awake!!");
        activateTravelers();
    }

    IEnumerator startDayCycle()
    {
        while (true)
        {
            UnityEngine.Debug.Log("repeat!");
            repeat = false;

            passDay();
            yield return new WaitUntil(() => repeat == true);
        }
    }

    void passDay()
    {
        //while (isTraveling)
        //{
        UnityEngine.Debug.Log("in pass day!");
            startParty();
            Invoke("stopParty", 2);
            Invoke("updateStats", 2);
            Invoke("setStats", 2);
            Invoke("resetLoop", 4);

            UnityEngine.Debug.Log("day passed!");
        //}
    }
    void createParty()
    {
        party[0] = one;
        party[1] = two;
        party[2] = three;
        party[3] = four;
        party[4] = five;
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
        StatsManager.updateFood();
        StatsManager.updateDistance();
        StatsManager.updateHealth();
    }

    void startParty()
    {
        for (int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStart();
        }
    }

    void stopParty()
    {
        for (int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStop();
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
        UnityEngine.Debug.Log("create function entered!");

        updateWindow.SetActive(true);
        updateText.text = message;
        exit = false;

        Time.timeScale = 0f;
        UnityEngine.Debug.Log("code still runs!");

        yield return new WaitUntil(() => exit == true);

        Time.timeScale = 1f;
        destroyUpdate();
    }

    void destroyUpdate()
    {
        UnityEngine.Debug.Log("destroy function entered!");
        //yield return new WaitUntil(() => exit == true);
        UnityEngine.Debug.Log("wait time passed!");
        updateWindow.SetActive(false);
        updateText.text = "";
    }

    public void playerDeath(int i)
    {
        UnityEngine.Debug.Log("die!");
        
        party[i].SetActive(false);
        activeParty[i] = false;
        startUpdate("Traveler " + (i+1) + " has been lost to the Darkness.");
    }

    
    void activateTravelers()
    {
        UnityEngine.Debug.Log("in activation...");
        for (int i = 0; i < activeParty.Length; i++)
        {
            UnityEngine.Debug.Log("== false");
            if (activeParty[i] == false)
            {
                UnityEngine.Debug.Log("deactviate!!");
                party[i].SetActive(false);
            }
        }
    }

    /*TO DO:
     * Make travel cycle pause when an update fires
     * 
     * 
     */
}
