using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chestCobtroller : MonoBehaviour
{
    public TMP_Text textPrice;
    public ParticleSystem particleSystem;
    public GameStateSO stateSO;

    Collider2D collider2D;
    Animator animator;

    bool isPlayerNear = false;
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        textPrice.text = $"${stateSO.chestPrice}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine(OpenChest());

                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Dash")
        {
            textPrice.enabled = true;
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Dash")
        {
            isPlayerNear = false;
            textPrice.enabled = false;
        }
    }

    IEnumerator OpenChest()
    {
        animator.SetTrigger("Open");
        collider2D.enabled = false;
        isPlayerNear = false;
        textPrice.enabled = false;

        particleSystem.Emit(45);

        stateSO.moneyCurrent -= stateSO.chestPrice;
        yield return new WaitForSeconds(0.8f);
        Instantiate(stateSO.prefabBonus, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
        yield return null;
    }
}
