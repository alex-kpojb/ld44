using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class turretController : MonoBehaviour
{
    public GameStateSO stateSO;
    public float dmg = 40;
    public ParticleSystem particle;

    Transform playerGO;
    public Transform weaponGO;

    public float reloadDelay = 2.4f;
    public float bulletSpeed = .15f;

    Coroutine coroutine;
    void Start()
    {
        playerGO = FindObjectOfType<playerController>().gameObject.transform;
        coroutine = StartCoroutine(AimAndFight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dash")
        {
            StopCoroutine(coroutine);
            stateSO.moneyCurrent += dmg;
            stateSO.mobsCurrentCounter--;
            particle.Emit(35);
            GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].color = Color.clear;
            Destroy(gameObject, 1);
        }
    }

    IEnumerator AimAndFight()
    {
        while (true)
        {
            weaponGO.right = playerGO.position - weaponGO.position;
            yield return new WaitForSeconds(reloadDelay);

            Instantiate(stateSO.prefabBullet, transform.position, weaponGO.rotation);
            
        }
    }
}
