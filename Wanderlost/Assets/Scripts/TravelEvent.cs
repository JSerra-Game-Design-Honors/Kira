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

    IEnumerator passDay()
    {
        //while (isTraveling)
        //{
            startParty();
            Invoke("stopParty", 2);
            Invoke("updateStats", 2);
            Invoke("setStats", 2);

        UnityEngine.Debug.Log("in pass day!");

        yield return new WaitUntil(() => complete == true);
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

    public void createUpdate(string message)
    {
        UnityEngine.Debug.Log("create function entered!");
        updateWindow.SetActive(true);
        updateText.text = message;

        exit = false;
        StartCoroutine(destroyUpdate());
    }

    IEnumerator destroyUpdate()
    {
        UnityEngine.Debug.Log("destroy function entered!");
        yield return new WaitUntil(() => exit == true);
        UnityEngine.Debug.Log("wait time passed!");
        updateWindow.SetActive(false);
        updateText.text = "";
    }

    /*TO DO:
     * Make travel cycle pause when an update fires
     * 
     * 
     */
}
