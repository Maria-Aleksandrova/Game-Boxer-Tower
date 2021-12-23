using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        print("game owfm;slkdjf");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        print("Игра закрылась!");
        Application.Quit();
    }
}
