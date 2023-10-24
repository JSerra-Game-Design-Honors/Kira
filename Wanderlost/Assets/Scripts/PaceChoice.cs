using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PaceChoice : MonoBehaviour
{
    public TMP_Text prompt;
    public TMP_Text currently;
    public int input = -1;
    string tempPace;

    // Start is called before the first frame update
    void Start()
    {
        prompt.text = "Choice: _";
        currently.text = "We are currently traveling at a "+StatsManager.pace+" pace.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            if (input != -1)
            {
                //Debug.Log(input);
                switch (input)
                {
                    case 1:
                        Debug.Log("Changing to steady");
                        tempPace = "steady";
                        break;
                    case 2:
                        Debug.Log("Changing to strenuous");
                        tempPace = "strenuous";
                        break;

                    case 3:
                        Debug.Log("Changing to grueling");
                        tempPace = "grueling";
                        break;
                }
                StatsManager.changePace(tempPace);
                Debug.Log(StatsManager.pace);
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (!(Input.GetKeyDown("delete") || Input.GetKeyDown("backspace")))
            {
                if (Input.GetKeyDown("1"))
                {
                    input = 1;
                    prompt.text = "Choice: 1";
                }
                else if (Input.GetKeyDown("2"))
                {
                    input = 2;
                    prompt.text = "Choice: 2";
                }
                else if (Input.GetKeyDown("3"))
                {
                    input = 3;
                    prompt.text = "Choice: 3";
                }
            }
            else
            {
                input = -1;
                prompt.text = "Choice: _";
            }
        }
    }
}
