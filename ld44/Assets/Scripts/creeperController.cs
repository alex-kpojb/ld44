using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creeperController : MonoBehaviour
{
    public GameStateSO stateSO;
    public GameObject agroGO;
    public ParticleSystem particleBoom;
    public ParticleSystem particleBlood;
    public ParticleSystem particleBoomSprited;

    public float dmg = 40;
    public float speed = 0.05f;
    public float delayBeforeBoom = 1f;
    public float agroRadius = 2f;
    float agroSpeed;
    public float boomRadius = 5f;
    public float boomForce = 2000f;

    GameObject player;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;

    bool isKilled = false;
    bool isTriggered = false;
    bool isCounted = false;

    Direction currentDirection;
    


    enum Direction
    {
        Left,
        Right,
        Player
    }

    private void OnEnable()
    {
        speed = Random.Range(speed - 0.01f, speed + (0.05f * (stateSO.currentWave+1)));
        agroSpeed = (float)(speed + (speed * 0.7));
        currentDirection = Direction.Right;
    }
    void Start()
    {
        currentDirection = Direction.Right;

        player = FindObjectOfType<playerController>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        agroGO.GetComponent<CircleCollider2D>().radius = agroRadius;
    }


    private void FixedUpdate()
    {
        //Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(0.6f, -1f), Color.yellow);

        if (currentDirection == Direction.Right)
        {
            if (Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0.6f, -1f), LayerMask.GetMask("Ground")))
            {
                transform.Translate(new Vector2(1 * speed, 0));
            }
            else
                currentDirection = Direction.Left;
        }
        if (currentDirection == Direction.Left)
        {
            if (Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-0.6f, -1f), LayerMask.GetMask("Ground")))
            {
                transform.Translate(new Vector2(-1 * speed, 0));
            }
            else
                currentDirection = Direction.Right;
        }
        if (currentDirection == Direction.Player)
        {
            var dir = (player.transform.position - transform.position).normalized;

            transform.Translate(new Vector2(dir.x, 0)* speed, 0);
        }

            
        }

    // Update is called once per frame
    void Update()
    {
        switch (currentDirection)
        {
            case Direction.Left: spriteRenderer.flipX = true; break;
            case Direction.Right: spriteRenderer.flipX = false; break;
            case Direction.Player:
                {
                    Vector2 estimatedVector = (Vector2)player.transform.position - (Vector2)transform.position;

                    if (estimatedVector.magnitude > 0.1)
                    {
                        spriteRenderer.flipX = (estimatedVector.normalized.x < 0) ? true : false;
                    }
                }
                ; break;
        }



    }

    public void Explode()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        currentDirection = Direction.Player;
        speed = agroSpeed;

        yield return new WaitForSeconds(delayBeforeBoom);

        if (isKilled)
            yield break;

        spriteRenderer.enabled = false;
        particleBoomSprited.Emit(1);
        particleBoom.Emit(50);
        collider2D.enabled = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boomRadius);

        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddExplosionForce(boomForce, this.transform.position, boomRadius);

                if (rb.gameObject.tag == "Player")
                {
                    stateSO.reduceMoney(dmg);
                    player.GetComponent<playerController>().particleBlood.Emit((int)dmg);
                }
            }
        }
        isKilled = true;
        CounterTrigger();
        Destroy(gameObject, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dash" & isTriggered == false)
        {
            isTriggered = true;
            isKilled = true;
            GetComponent<Collider2D>().enabled = false;
            stateSO.moneyCurrent += dmg;
            CounterTrigger();
            particleBlood.Emit(45);
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(gameObject, 1);
        }
    }

    void CounterTrigger()
    {
        if (isCounted)
            return;

        isCounted = true;
        stateSO.mobsCurrentCounter--;
    }
}
