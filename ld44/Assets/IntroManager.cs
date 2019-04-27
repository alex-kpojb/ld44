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
}
