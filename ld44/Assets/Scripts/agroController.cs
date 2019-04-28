using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agroController : MonoBehaviour
{
    GameObject creeper;
    creeperController creeperController;
    void Start()
    {
        creeper = transform.parent.gameObject;
        creeperController = creeper.GetComponent<creeperController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            creeperController.Explode();
        }
    }
}
