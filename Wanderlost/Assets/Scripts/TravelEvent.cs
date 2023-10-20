using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TravelEvent : MonoBehaviour
{
    [SerializeField]
    GameObject one, two, three, four, five;

    GameObject[] party = new GameObject[5];
    bool isTraveling;

    public TMP_Text promptText;
    public TMP_Text statsText;

    string[] seasonsSet = {"Summer", "Autumn", "Winter", "Spring"};

    //STATS - **PLACEHOLDER STATS... TO BE REPLACED**
    int day = 1;
    int seasonNum = 0;
    string weather = "Warm";
    string health = "Good";
    int food = 500;
    int nextWay = 100;


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
        Debug.Log("day passed!");
        for(int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStart();
        }

        //stats
        updateStats();
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
        statsText.text = "Day " + day + " of " + seasonsSet[seasonNum] + "\nWeather: " + weather + "\nHealth: " + health + "\nFood: " + food + " portions\nNext Wayfinder: " + nextWay + " paces";
    }

    void updateStats()
    {
        updateDate();
    }

    void updateDate()
    {
        day += 1;
        if (day > 100)
        {
            day = 1;
            if (seasonNum == 3)
            {
                seasonNum = 0;
            }
            else
            {
                seasonNum++;
            }
        }
    }
}
