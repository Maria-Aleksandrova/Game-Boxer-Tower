using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour {

    public delegate void GameOverDelegate ();
    public static event GameOverDelegate GameOverEvent;
    bool gameOver = false;

    private void Start()
    {
        GameOverEvent += GameOver;
    }

    void GameOver()
    {
        gameOver = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>())
            {
                if(!collision.gameObject.GetComponent<Rigidbody2D>().isKinematic)
                {
                    Debug.Log(collision.name);
                    GameOverEvent();
                }
            }
        }
    }


}
