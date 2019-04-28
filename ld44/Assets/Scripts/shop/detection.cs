using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection : MonoBehaviour
{

    public bool detected;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            detected = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
}
