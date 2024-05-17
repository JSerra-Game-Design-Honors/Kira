using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using UnityEngine.Animations;

public class WalkScript : MonoBehaviour
{
    //bool traveling = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
        {
            traveling = !traveling;
            if (!traveling)
            {
                UnityEngine.Debug.Log("Stop!");
                animator.speed = 0;
            }
            else
            {
                UnityEngine.Debug.Log("Start!");
                animator.speed = 1;
            }
        }*/
    }

    public void walkStart()
    {
        //UnityEngine.Debug.Log("Start!");
        animator.speed = 1;
    }

    public void walkStop()
    {
        animator.speed = 0;
    }
}
