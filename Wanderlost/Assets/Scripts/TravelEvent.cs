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
        promptText.text = "Press <color=#00cbff>SPACE</color> to continue.";
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

    void resetLoop()
    {
        repeat = true;
    }

    public void deletePlayer(int playerNum)
    {
        if(playerNum == 1)
        {
            one.SetActive(false);
        } 
        else if (playerNum == 2)
        {
            two.SetActive(false);
        }
        else if (playerNum == 3)
        {
            three.SetActive(false);
        }
        else if (playerNum == 4)
        {
            four.SetActive(false);
        }
        else
        {
            five.SetActive(false);
        }
    }
}
