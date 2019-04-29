using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;


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
        StartCoroutine(GameOverCoroutine());     
    }

    IEnumerator Fade(Image componentToFade, float from, float to, float lerpTime) {
        //using basic Lerp logic here
        float t = 0f;
        Color32 cachedColor = componentToFade.color;

        while (true) {
            if (t >= lerpTime)
                t = lerpTime;

            //change alpha value and assign the value
            //the core of the function
            cachedColor.a = (byte)Mathf.Lerp(from, to, t / lerpTime);
            componentToFade.color = cachedColor;

            if (t >= lerpTime)
                break;
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator GameOverCoroutine() {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        toScene(0);
        yield return null;
    }
}
