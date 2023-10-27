using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class TravelEvent : MonoBehaviour
{
    [SerializeField]
    GameObject one, two, three, four, five;

    GameObject[] party = new GameObject[5];
    bool isTraveling;

    public TMP_Text promptText;
    public TMP_Text statsText;

    bool repeat;

    // Start is called before the first frame update
    void Start()
    {
        createParty();
        setStats();
        promptText.text = "Press SPACE to continue.";
        isTraveling = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("we are updating wohoo!");
        if (Input.GetKeyDown("space"))
        {
            isTraveling = !isTraveling;
        
        if (isTraveling)
        {
            promptText.text = "Press SPACE to stop.";

            passDay();
            //Debug.Log("space on");
        }
        else
        {
            promptText.text = "Press SPACE to continue.";
            //Debug.Log("space off");
        }
        }
    }

    void passDay()
    {
        //while (isTraveling)
        //{
            startParty();
            updateStats();
            Invoke("stopParty", 4);
            Invoke("setStats", 4);

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
        statsText.text = "Day " + StatsManager.day + " of " + StatsManager.seasonsSet[StatsManager.seasonNum] + "\nWeather: " + StatsManager.weather + "\nHealth: " + StatsManager.health + "\nFood: " + StatsManager.food + " portions\nNext Wayfinder: " + StatsManager.nextWay + " leagues";
    }

    void updateStats()
    {
        StatsManager.updateDate();
        StatsManager.chooseWeather();
        StatsManager.updateHealth();
        StatsManager.updateFood();
        StatsManager.updateDistance();
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
}
