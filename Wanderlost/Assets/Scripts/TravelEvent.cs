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

    // Start is called before the first frame update
    void Start()
    {
        createParty();
        setStats();
        Invoke("updateStats", 3);
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
            //Debug.Log("space on");
            passDay();
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
        UnityEngine.Debug.Log("day passed!");
        for(int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStart();
        }

        //stats
        UnityEngine.Debug.Log("waiting...");
        updateStats();
        UnityEngine.Debug.Log("done!");
        setStats();

        for (int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStop();
        }
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
    }
}
