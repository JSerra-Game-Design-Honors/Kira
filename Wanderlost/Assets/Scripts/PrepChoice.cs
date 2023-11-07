using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrepChoice : MonoBehaviour
{
    public TMP_Text prompt;
    public TMP_Text stats;
    public int input = -1;

    // Start is called before the first frame update
    void Start()
    {
        prompt.text = "Choice: _";
        stats.text = "Weather: <color=#ff0083>" + StatsManager.weather+ "</color>\nHealth: <color=#ff0083>" + StatsManager.health+ "</color>\nPace: <color=#ff0083>" + StatsManager.pace+ "</color>\nRations: <color=#ff0083>" + StatsManager.rations+ "</color>";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            if(input != -1)
            {
                //Debug.Log(input);
                switch (input)
                {
                    case 1:
                        Debug.Log("Activating Choice 1");
                        SceneManager.LoadScene(1);
                        break;

                    case 2:
                        Debug.Log("Activating Choice 2");
                        break;

                    case 3:
                        Debug.Log("Activating Choice 3");
                        break;
                    case 4:
                        Debug.Log("Activating Choice 4");
                        SceneManager.LoadScene(2);
                        break;

                    case 5:
                        Debug.Log("Activating Choice 5");
                        SceneManager.LoadScene(3);
                        break;

                    case 6:
                        Debug.Log("Activating Choice 6");
                        break;

                    case 7:
                        Debug.Log("Activating Choice 7");
                        break;

                    case 8:
                        Debug.Log("Activating Choice 8");
                        break;
                }
            }
        }
        else
        {
            if (!(Input.GetKeyDown("delete") || Input.GetKeyDown("backspace")))
            {
                if (Input.GetKeyDown("1"))
                {
                    //Debug.Log("choice ooone!");
                    input = 1;
                    prompt.text = "Choice: <color=#00cbff>1</color>";
                }
                else if (Input.GetKeyDown("2"))
                {
                    input = 2;
                    prompt.text = "Choice: <color=#00cbff>2</color>";
                }
                else if (Input.GetKeyDown("3"))
                {
                    input = 3;
                    prompt.text = "Choice: <color=#00cbff>3</color>";
                }
                else if (Input.GetKeyDown("4"))
                {
                    input = 4;
                    prompt.text = "Choice: <color=#00cbff>4</color>";
                }
                else if (Input.GetKeyDown("5"))
                {
                    input = 5;
                    prompt.text = "Choice: <color=#00cbff>5</color>";
                }
                else if (Input.GetKeyDown("6"))
                {
                    input = 6;
                    prompt.text = "Choice: <color=#00cbff>6</color>";
                }
                else if (Input.GetKeyDown("7"))
                {
                    input = 7;
                    prompt.text = "Choice: <color=#00cbff>7</color>";
                }
                else if (Input.GetKeyDown("8"))
                {
                    input = 8;
                    prompt.text = "Choice: <color=#00cbff>8</color>";
                }
            }
            else
            {
                //Debug.Log("deleeete!");
                input = -1;
                prompt.text = "Choice: _";
            }
        }
    }
}
