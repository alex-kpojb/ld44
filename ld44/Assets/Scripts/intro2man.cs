using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class intro2man : MonoBehaviour
{
    public float letterPause = 0.05f;
    public float phrasePause = 2.5f;

    public string text1 = "Kill enemies - raise money";
    public string text2 = "Buy bonuses for money";
    public string text3 = "Zero money means DEATH!";
    public string text4 = "Are you ready?!?";

    public TMP_Text Text;
    public GameObject leftGO;
    public GameObject rightGO;
    public GameObject jumpGO;
    public GameObject dashGO;

    bool A = false;
    bool D = false;
    bool Space = false;
    bool Dash = false;
    // Start is called before the first frame update
    void Start()
    {
        Text.gameObject.SetActive(false);
        leftGO.SetActive(false);
        rightGO.SetActive(false);
        jumpGO.SetActive(false);
        dashGO.SetActive(false);
        StartCoroutine(StartIntro2());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            leftGO.SetActive(false);
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rightGO.SetActive(false);
            D = true;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpGO.SetActive(false);
            Space = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            dashGO.SetActive(false);
            Dash = true;
        }

    }

    IEnumerator StartIntro2() {
        leftGO.SetActive(true);
        rightGO.SetActive(true);
        yield return new WaitUntil(() => A & D);
        jumpGO.SetActive(true);
        yield return new WaitUntil(() => A & D & Space);
        dashGO.SetActive(true);
        yield return new WaitUntil(() => A & D & Space & Dash);
        Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(TypeText(text1));
        yield return StartCoroutine(TypeText(text2));
        yield return StartCoroutine(TypeText(text3));
        yield return StartCoroutine(TypeText(text4));

        yield return null;
    }

    IEnumerator TypeText(string message) {
        Text.text = "";
        foreach (char letter in message.ToCharArray()) {
            Text.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        yield return new WaitForSeconds(phrasePause);
    }
}
