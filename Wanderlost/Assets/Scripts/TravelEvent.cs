using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TravelEvent : MonoBehaviour
{
    public bool isTraveling;
    public TMP_Text prompt;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("hi");
            passDay();
        }
        else
        {

        }
    }

    void passDay()
    {

    }
}
