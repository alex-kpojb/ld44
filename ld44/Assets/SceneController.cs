using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
    public TMP_Text text6;
    public TMP_Text text7;
    public SpriteRenderer fadeImage;

    public int mainMenu = 0;
    public int intro = 1;
    public int shop = 2;
    public int firstLevel = 3;
    public int secondLevel = 4;
    public int bossFight = 5;

    public int winnerScreen = 6;
    public int looserScreen = 7;

    int currentScene = 0;

    bool isFirstLevelCOmpleted = false;
    bool isSecondLevelCOmpleted = false;

    bool isGameOver = false;
    bool isGameWin = false;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        switch (currentScene)
        {
            //mainMenu
            case 0: toScene(intro); break;
            //intro
            case 1: toScene(firstLevel); break;
            //shop
            case 2: {
                    if (isFirstLevelCOmpleted & isSecondLevelCOmpleted)
                        toScene(bossFight);
                    else if (isFirstLevelCOmpleted & !isSecondLevelCOmpleted)
                        toScene(secondLevel);
                    else if (!isFirstLevelCOmpleted & !isSecondLevelCOmpleted)
                        toScene(firstLevel);

                }; break;
            //1st level
            case 3: {
                    isFirstLevelCOmpleted = true;
                    toScene(shop);
                }; break;
            //2nd level
            case 4:{
                    isSecondLevelCOmpleted = true;
                    toScene(shop);
                }; break;
            //boss
            case 5: toScene(winnerScreen); break;
        }
    }

    public void toScene(int value) {
        StartCoroutine(FadeLoadSceneFade(value));
    }

    public void WinTheGame() {

    }

    public void GameOver() {
        if (isGameOver)
            return;

        isGameOver = true;
        StartCoroutine(GameOverCoroutine());     
    }


    IEnumerator FadeIn() {
        StartCoroutine(Fade(image1, 0, 255, 1f));
        StartCoroutine(Fade(image2, 0, 255, 1f));
        StartCoroutine(Fade(image3, 0, 255, 1f));
        StartCoroutine(Fade(image4, 0, 255, 1f));

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
        StartCoroutine(Fade(image1, 255, 0, 1f));
        StartCoroutine(Fade(image2, 255, 0, 1f));
        StartCoroutine(Fade(image3, 255, 0, 1f));
        StartCoroutine(Fade(image4, 255, 0, 1f));

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
        if (componentToFade != null)
        {
            float t = 0f;
            Color32 cachedColor = componentToFade.color;

            while (true)
            {
                if (t >= lerpTime)
                    t = lerpTime;

                cachedColor.a = (byte)Mathf.Lerp(from, to, t / lerpTime);
                componentToFade.color = cachedColor;

                if (t >= lerpTime)
                    break;
                t += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }

    IEnumerator Fade(Image componentToFade, float from, float to, float lerpTime)
    {
        if (componentToFade != null)
        {
            float t = 0f;
            Color32 cachedColor = componentToFade.color;

            while (true)
            {
                if (t >= lerpTime)
                    t = lerpTime;

                cachedColor.a = (byte)Mathf.Lerp(from, to, t / lerpTime);
                componentToFade.color = cachedColor;

                if (t >= lerpTime)
                    break;
                t += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
    IEnumerator Fade(SpriteRenderer componentToFade, float from, float to, float lerpTime) {
        if (componentToFade != null)
        {
            float t = 0f;
            Color32 cachedColor = componentToFade.color;

            while (true)
            {
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
        }
        yield return null;
    }
    IEnumerator GameOverCoroutine() {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.2f);
        toScene(1);
        yield return null;
    }

    IEnumerator FadeLoadSceneFade(int value)
    {
        yield return StartCoroutine(FadeOut());
        Time.timeScale = 1f;
        SceneManager.LoadScene(value);
        yield return StartCoroutine(FadeIn());
        yield return null;
    }
}
