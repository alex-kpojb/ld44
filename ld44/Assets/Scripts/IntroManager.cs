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
    int addon= 0;
    public void checkInputField1()
    {
      //  Debug.Log(cardNumberText.GetComponent<TextMeshProUGUI>().text.Length.ToString());
       // Debug.Log("input: " + cardNumberText.GetComponentInParent<TMP_InputField>().text);
        if (cardNumberText.GetComponent<TextMeshProUGUI>().text.Length > 16)
        {
            date1.GetComponentInParent<TMP_InputField>().ActivateInputField();
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
        }      
    }
    public void checkInputField3()
    {
        if (date2.GetComponent<TextMeshProUGUI>().text.Length >= 2)
        {
            cvc.GetComponentInParent<TMP_InputField>().ActivateInputField();
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
        playablePlayer.SetActive(false);
        cardNumberText.GetComponentInParent<TMP_InputField>().ActivateInputField();
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
        //playablePlayer.transform.position = Camera.main.ScreenToWorldPoint(player.transform.position);
        player.SetActive(false);
        fill.SetActive(false);
        checkout.SetActive(false);
        GetComponent<Animator>().SetBool("intro",true);
        fade.SetActive(false);
    }
}
