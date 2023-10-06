using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrepChoice : MonoBehaviour
{
    public TMP_Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        prompt.text = "Choice: _";
    }

    // Update is called once per frame
    void Update()
    {
        string input = Input.inputString;
        if (!(Input.GetKeyDown("delete") || Input.GetKeyDown("backspace")))
        {
            if (Input.GetKeyDown("1"))
            {
                //Debug.Log("choice ooone!");
                prompt.text = "Choice: 1";
            }
            else if (Input.GetKeyDown("2"))
            {
                prompt.text = "Choice: 2";
            }
            else if (Input.GetKeyDown("3"))
            {
                prompt.text = "Choice: 3";
            }
            else if (Input.GetKeyDown("4"))
            {
                prompt.text = "Choice: 4";
            }
            else if (Input.GetKeyDown("5"))
            {
                prompt.text = "Choice: 5";
            }
            else if (Input.GetKeyDown("6"))
            {
                prompt.text = "Choice: 6";
            }
            else if (Input.GetKeyDown("7"))
            {
                prompt.text = "Choice: 7";
            }
            else if (Input.GetKeyDown("8"))
            {
                prompt.text = "Choice: 8";
            }
        }
        else
        {
            //Debug.Log("deleeete!");
            prompt.text = "Choice: _";
        }
    }
}
