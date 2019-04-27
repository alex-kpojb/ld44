using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour
{
    public GameStateSO stateSO;
    public ParticleSystem particle;
    Collider2D collider2D;

    public float rotationRatio = 1f;
    public float speed = 4f;
    public float dmg = 20f;

    GameObject player;


    private void OnEnable()
    {
        speed = Random.Range(speed - 1.2f, speed + (0.8f * stateSO.currentWave));
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<playerController>().gameObject;
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 estimatedVector = (Vector2)player.transform.position - (Vector2)transform.position;

        if (estimatedVector.magnitude > 0.3)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                player.transform.position,
                speed / estimatedVector.magnitude * Time.deltaTime);
        }
        else
        {
            //attack animation
        }

        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.tag == "Player")
        {
            stateSO.moneyCurrent -= dmg;
            Vector2[] hitVectors = new Vector2[] { Vector2.left, Vector2.right};
            Vector2 hitForce = hitVectors[Random.Range(0,2)];
            player.GetComponent<Rigidbody2D>().AddForce(hitForce * dmg * 10);
            player.GetComponent<playerController>().particleBlood.Emit((int)dmg);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dash")
        {
            stateSO.moneyCurrent += dmg;
            stateSO.mobsCurrentCounter--;
            EmitParticle();
            collider2D.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(gameObject,1);
        }
    }

    void EmitParticle()
    {
        particle.Emit(25);
    }
}
