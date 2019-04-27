using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creeperController : MonoBehaviour
{
    public GameStateSO stateSO;
    public GameObject agroGO;
    public float dmg = 40;
    public ParticleSystem particleBoom;
    public ParticleSystem particleBlood;

    public float delayBeforeBoom = 1f;

    GameObject player;
    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(delayBeforeBoom);
        spriteRenderer.enabled = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f);
        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                //rb add explosion force 2d
                //rb.Add
            }
        }

        particleBoom.Emit(50);
        Destroy(gameObject, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dash")
        {
            stateSO.moneyCurrent += dmg;
            stateSO.mobsCurrentCounter--;
            particleBlood.Emit(45);
            GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(gameObject, 1);
        }
    }
}
