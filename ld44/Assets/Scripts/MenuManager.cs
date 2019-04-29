using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneController.instance.NextScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
