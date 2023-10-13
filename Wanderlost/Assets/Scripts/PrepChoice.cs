using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrepChoice : MonoBehaviour
{
    public TMP_Text prompt;
    public int input = -1;

    // Start is called before the first frame update
    void Start()
    {
        prompt.text = "Choice: _";
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
                        break;

                    case 5:
                        Debug.Log("Activating Choice 5");
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
                else if (Input.GetKeyDown("4"))
                {
                    input = 4;
                    prompt.text = "Choice: 4";
                }
                else if (Input.GetKeyDown("5"))
                {
                    input = 5;
                    prompt.text = "Choice: 5";
                }
                else if (Input.GetKeyDown("6"))
                {
                    input = 6;
                    prompt.text = "Choice: 6";
                }
                else if (Input.GetKeyDown("7"))
                {
                    input = 7;
                    prompt.text = "Choice: 7";
                }
                else if (Input.GetKeyDown("8"))
                {
                    input = 8;
                    prompt.text = "Choice: 8";
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
