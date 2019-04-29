using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
    public TMP_Text text6;
    public TMP_Text text7;
    public SpriteRenderer fadeImage;

    bool isGameOver = false;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toScene(int value) {
        SceneManager.LoadScene(value);
    }


    public void GameOver() {
        if (isGameOver)
            return;

        isGameOver = true;
        StartCoroutine(GameOverCoroutine());     
    }


    IEnumerator FadeIn() {
        StartCoroutine(Fade(text1, 0, 255, 1f));
        StartCoroutine(Fade(text2, 0, 255, 1f));
        StartCoroutine(Fade(text3, 0, 255, 1f));
        StartCoroutine(Fade(text4, 0, 255, 1f));
        StartCoroutine(Fade(text5, 0, 255, 1f));
        StartCoroutine(Fade(text6, 0, 255, 1f));
        StartCoroutine(Fade(text7, 0, 255, 1f));
        yield return StartCoroutine(Fade(fadeImage, 255, 0, 1f));
    }
    IEnumerator FadeOut() {
        StartCoroutine(Fade(text1, 255, 0, 1f));
        StartCoroutine(Fade(text2, 255, 0, 1f));
        StartCoroutine(Fade(text3, 255, 0, 1f));
        StartCoroutine(Fade(text4, 255, 0, 1f));
        StartCoroutine(Fade(text5, 255, 0, 1f));
        StartCoroutine(Fade(text6, 255, 0, 1f));
        StartCoroutine(Fade(text7, 255, 0, 1f));
        yield return StartCoroutine(Fade(fadeImage, 0, 255, 1f));
        //yield return new WaitForSeconds(2f);
    }

    IEnumerator Fade(TMP_Text componentToFade, float from, float to, float lerpTime) {

        float t = 0f;
        Color32 cachedColor = componentToFade.color;

        while (true) {
            if (t >= lerpTime)
                t = lerpTime;

            cachedColor.a = (byte)Mathf.Lerp(from, to, t / lerpTime);
            componentToFade.color = cachedColor;

            if (t >= lerpTime)
                break;
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    IEnumerator Fade(SpriteRenderer componentToFade, float from, float to, float lerpTime) {

        float t = 0f;
        Color32 cachedColor = componentToFade.color;

        while (true) {
            if (t >= lerpTime)
                t = lerpTime;

            var a = Mathf.Lerp(from, to, t / lerpTime);
            cachedColor.a = (byte)Mathf.Lerp(from, to, t / lerpTime);
            componentToFade.color = cachedColor;

            if (t >= lerpTime)
                break;
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    IEnumerator GameOverCoroutine() {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeOut());
        toScene(0);
        yield return null;
    }
}
