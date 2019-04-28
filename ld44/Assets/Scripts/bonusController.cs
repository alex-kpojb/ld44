using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusController : MonoBehaviour
{
    public BonusSO bonusSO;
    public GameStateSO stateSO;

    public ParticleSystem particleSystem;

    SpriteRenderer spriteRenderer;
    Collider2D collider2D;
    
    public string name;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        var b = bonusSO.Bonuses[Random.Range(0, bonusSO.Bonuses.Count)];

        spriteRenderer.sprite = b.sprite;
        name = b.name;
        value = b.value;
    }

    private void Update()
    {
        var newRotation = Time.deltaTime * 180;
        transform.RotateAround(transform.position, Vector3.up, newRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Dash")
        {
            ApplyBonus(name, value);
            particleSystem.Emit(18);
            collider2D.enabled = false;
            spriteRenderer.color = Color.clear;
            Destroy(gameObject,1);
        }
    }

    void ApplyBonus(string name, float value)
    {
        switch (name)
        {
            case "jumpForce":stateSO.jumpForce += value; break;
            case "maxJumps": stateSO.maxJumps += value; break;
            case "walkForce":stateSO.walkForce += value; break;
            case "dashMax":stateSO.dashMax += value; break;
            case "dashTime":stateSO.dashTime += value; break;
            case "dashSpeed":stateSO.dashSpeed += value; break;
        }
    }
}
