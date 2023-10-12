using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelEvent : MonoBehaviour
{
    public bool isTraveling;
    // Start is called before the first frame update
    void Start()
    {
        isTraveling = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTraveling)
        {
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
