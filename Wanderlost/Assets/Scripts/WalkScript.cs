using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WalkScript : MonoBehaviour
{
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            anim.Play("AnimationTravelerWalk");
        }else
        {
            anim.Play("AnimationTravelerIdle");
        }
    }
}
