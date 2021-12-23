using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasManager : MonoBehaviour {

    [SerializeField] Canvas canvas;

    private void Start()
    {
        GameOverZone.GameOverEvent += GameOver;
    }

    void GameOver()
    {
        canvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
