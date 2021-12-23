using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField] Text scoreText;

    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    private void Start()
    {
        GameOverZone.GameOverEvent += WriteScore;
    }

    void WriteScore()
    {
        scoreText.text += score;
    }
}
