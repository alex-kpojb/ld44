using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pikesController : MonoBehaviour
{
    public float dmg = 5f;
    public GameStateSO stateSO;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.tag == "Player")
        {
            stateSO.reduceMoney(dmg);
            Vector2[] hitVectors = new Vector2[] { Vector2.left, Vector2.right };
            Vector2 hitForce = hitVectors[Random.Range(0, 2)];
            player.GetComponent<Rigidbody2D>().AddForce(hitForce * dmg * 60);
            player.GetComponent<playerController>().particleBlood.Emit((int)dmg);
        }
    }
}
