using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopkeeperScript : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x)
        {
            anim.SetBool("angry", true);
        }
        else
        {
            anim.SetBool("angry", false);
        }
    }
}
