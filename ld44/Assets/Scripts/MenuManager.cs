using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameStateSO stateSO;
    public void Play()
    {
        SceneController.instance.NextScene();
    }

    public void Quit()
    {
        Application.Quit();
    }

    //restart the game
    public void MENU()
    {
        stateSO.Reset();
        SceneManager.LoadScene(3);
    }
}
