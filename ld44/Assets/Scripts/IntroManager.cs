using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public float letterPause = 0.2f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;
    public GameObject dashTarget;

    public string text1 = "welcome my credit card";
    public string text2 = "";
    public string text3 = "";
    public string text4 = "";
    public string text5 = "";
    public string text6 = "";
    public string text7 = "";
    public string text8 = "";
    public string text9 = "";
    public string text10 = "";
    public string text11 = "";
/*
    public void call1()
    {
        textField.text = "";
        string fieldtext = text1;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call2()
    {
        textField.text = "";
        string fieldtext = text2;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call3()
    {
        textField.text = "";
        string fieldtext = text3;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call4()
    {
        textField.text = "";
        string fieldtext = text4;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call5()
    {
        textField.text = "";
        string fieldtext = text5;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call6()
    {
        textField.text = "";
        string fieldtext = text6;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
        dashTarget.SetActive(true);
        StartCoroutine(waitForTargets());
    }
    public void call7()
    {
        textField.text = "";
        string fieldtext = text7;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call8()
    {
        textField.text = "";
        string fieldtext = text8;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call9()
    {
        textField.text = "";
        string fieldtext = text9;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call10()
    {
        textField.text = "";
        string fieldtext = text10;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    public void call11()
    {
        textField.text = "";
        string fieldtext = text11;
        text1 = "";
        StartCoroutine(TypeText(fieldtext));
    }
    */


    IEnumerator TypeText(string message)
    {
        foreach (char letter in message.ToCharArray())
        {
            textField.text += letter;         
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    IEnumerator waitForTargets()
    {
        yield return new WaitUntil(() => dashTarget == null);
        GetComponent<Animator>().SetBool("continue", true);
        StopAllCoroutines();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public GameObject cardNumberText;
    public GameObject date1;
    public GameObject date2;
    public GameObject cvc;
    int dir = 1;
    int prevdir = 0;
    bool k;
    int index;
    int addon= 0;

    private void Update()
    {
        if(index == 0)
        {
            cardNumberText.GetComponentInParent<TMP_InputField>().ActivateInputField();
        }
        if(index == 1)
        {
            date1.GetComponentInParent<TMP_InputField>().ActivateInputField();
        }
        if (index == 2)
        {
            date2.GetComponentInParent<TMP_InputField>().ActivateInputField();
        }
        if (index == 3)
        {
            cvc.GetComponentInParent<TMP_InputField>().ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            leftGO.SetActive(false);
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rightGO.SetActive(false);
            D = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpGO.SetActive(false);
            Space = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            dashGO.SetActive(false);
            Dash = true;
        }
    }
    public void checkInputField1()
    {
      //  Debug.Log(cardNumberText.GetComponent<TextMeshProUGUI>().text.Length.ToString());
       // Debug.Log("input: " + cardNumberText.GetComponentInParent<TMP_InputField>().text);
        if (cardNumberText.GetComponent<TextMeshProUGUI>().text.Length > 16)
        {
            date1.GetComponentInParent<TMP_InputField>().ActivateInputField();
            index++;
            return;
        }
        // Debug.Log("iffissä: " + (float)(cardNumberText.GetComponentInParent<TMP_InputField>().text.Length+addon) / 5);
        if ((cardNumberText.GetComponentInParent<TMP_InputField>().text.Length-addon) / 5 >= dir)
        {
            if (k) return;
            k = true;
            cardNumberText.GetComponentInParent<TMP_InputField>().MoveTextEnd(false);
            cardNumberText.GetComponentInParent<TMP_InputField>().text  += "-";
            cardNumberText.GetComponentInParent<TMP_InputField>().MoveTextEnd(false);
            addon++;
            // Debug.Log(cardNumberText.GetComponent<TextMeshProUGUI>().text);
            dir++;
        }
        k = false;
    }
    public void checkInputField2()
    {
        if (date1.GetComponent<TextMeshProUGUI>().text.Length >= 2)
        {
            date2.GetComponentInParent<TMP_InputField>().ActivateInputField();
            index++;
        }      
    }
    public void checkInputField3()
    {
        if (date2.GetComponent<TextMeshProUGUI>().text.Length >= 2)
        {
            cvc.GetComponentInParent<TMP_InputField>().ActivateInputField();
            index++;
        }     
    }
    public void checkInputField4()
    {
        if (cvc.GetComponent<TextMeshProUGUI>().text.Length >= 3)
        {
            Reveal();
            cvc.GetComponentInParent<TMP_InputField>().DeactivateInputField();
        }
    }
    private void Start()
    {
        PlayerPrefs.SetInt("1", 1);
        PlayerPrefs.SetInt("2", 1);
        PlayerPrefs.SetInt("3", 1);
        PlayerPrefs.SetInt("4", 1);
        PlayerPrefs.SetInt("5", 1);
        PlayerPrefs.SetInt("6", 1);

        //activate CVC
        index = 3;

        playablePlayer.SetActive(false);
        //cardNumberText.GetComponentInParent<TMP_InputField>().ActivateInputField();

        Text.gameObject.SetActive(false);
        leftGO.SetActive(false);
        rightGO.SetActive(false);
        jumpGO.SetActive(false);
        dashGO.SetActive(false);
    }

    public GameObject player;
    public GameObject fade;
    public GameObject playablePlayer;
    public GameObject checkout;
    public GameObject fill;
    void Reveal()
    {
        player.SetActive(true);
        fade.GetComponent<Animator>().SetBool("fade", true);
        StartCoroutine(startIntro());
    }

    IEnumerator startIntro()
    {
        yield return new WaitForSeconds(3);
        playablePlayer.SetActive(true);
        index++;
        player.SetActive(false);
        fill.SetActive(false);
        checkout.SetActive(false);
        GetComponent<Animator>().SetBool("intro",true);
        fade.SetActive(false);
        StartCoroutine(StartIntro2());
    }

    public float letterPause2 = 0.05f;
    public float phrasePause = 2.5f;

    public string text_1 = "Kill enemies - raise money";
    public string text_2 = "Buy bonuses with money";
    public string text_3 = "Zero money means DEATH!";
    public string text_4 = "Are you ready?!?";

    public TMP_Text Text;
    public GameObject leftGO;
    public GameObject rightGO;
    public GameObject jumpGO;
    public GameObject dashGO;

    bool A = false;
    bool D = false;
    bool Space = false;
    bool Dash = false;


    IEnumerator StartIntro2()
    {
        A = false;
        D = false;
        Space = false;
        Dash = false;

        leftGO.SetActive(true);
        rightGO.SetActive(true);
        yield return new WaitUntil(() => A & D);
        jumpGO.SetActive(true);
        yield return new WaitUntil(() => A & D & Space);
        dashGO.SetActive(true);
        yield return new WaitUntil(() => A & D & Space & Dash);
        Text.text = "";
        Text.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        if(!doing)
        {
            doing = true;
            yield return StartCoroutine(TypeText2(text_1));
            yield return StartCoroutine(TypeText2(text_2));
            yield return StartCoroutine(TypeText2(text_3));
            yield return StartCoroutine(TypeText2(text_4));
            SceneController.instance.NextScene();
            doing = false;
        }
        //SceneManager.LoadScene(2);       
        yield return null;
    }

    IEnumerator TypeText2(string message)
    {
        Text.text = "";
        foreach (char letter in message.ToCharArray())
        {
            Text.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause2);
        }
        yield return new WaitForSeconds(phrasePause);
    }
    static bool doing = false;
}
