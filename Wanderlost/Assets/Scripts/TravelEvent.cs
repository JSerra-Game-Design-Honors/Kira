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
    public TMP_Text prompt;

    //STATS
    int days = 1;
    string season = "Summer";
    string weather;
    string health1;


    // Start is called before the first frame update
    void Start()
    {
        createParty();
        prompt.text = "Press SPACE to continue.";
        isTraveling = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("we are updating wohoo!");
        if (Input.GetKeyDown("space"))
        {
            isTraveling = !isTraveling;
        }
        if (isTraveling)
        {
            prompt.text = "Press SPACE to stop.";
            //Debug.Log("space on");
            passDay();
        }
        else
        {
            prompt.text = "Press SPACE to continue.";
            //Debug.Log("space off");
        }
    }

    void passDay()
    {
        for(int i = 0; i < party.Length; i++)
        {
            party[i].GetComponent<WalkScript>().walkStart();
        }

        //stats

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
}
