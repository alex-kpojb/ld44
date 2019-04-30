using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossController : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float dmg = 10;
    public float playerDmg = 1;
    public float speed = 5f;
    public ParticleSystem particles;
    public GameStateSO stateSO;
    public Slider slider;

    float maxBossHealth = 40;
    float currentHP;
    void Start()
    {
        player = FindObjectOfType<playerController>().gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        currentHP = maxBossHealth;
    }

    private void FixedUpdate()
    {
        Vector2 estimatedVector = (Vector2)player.transform.position - (Vector2)transform.position;

        if (estimatedVector.magnitude > 0.1)
        {
            spriteRenderer.flipX = (estimatedVector.normalized.x > 0) ? true : false;
        }

        if (estimatedVector.magnitude > 0.3)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                player.transform.position,
                speed / estimatedVector.magnitude * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
            slider.value = currentHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.tag == "Player")
        {
            StartCoroutine(Attack());
        }

        if (collision.tag == "Dash")
        {
            Hit(playerDmg);
        }
    }

    void Hit(float value)
    {
        if (currentHP - value <= 0)
        {
            currentHP = 0;
            particles.Emit(1000);
            GetComponent<Collider2D>().enabled = false;
            animator.enabled = false;
            spriteRenderer.color = Color.clear;
            spriteRenderer.enabled = false;
            Destroy(gameObject, 1);

            SceneController.instance.NextScene();
        }
        else
        {
            currentHP -= value;
            particles.Emit(100);
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.03f);
        stateSO.reduceMoney(dmg);
        Vector2[] hitVectors = new Vector2[] { Vector2.left, Vector2.right };
        Vector2 hitForce = hitVectors[Random.Range(0, 2)];
        player.GetComponent<Rigidbody2D>().AddForce(hitForce * dmg * 10);
        player.GetComponent<playerController>().particleBlood.Emit((int)dmg);
        yield return null;
    }

}
