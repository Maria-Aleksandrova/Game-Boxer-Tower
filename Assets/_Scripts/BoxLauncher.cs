using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLauncher : MonoBehaviour {

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject[] _boxPrefabs;
    [SerializeField] BoxesManager _boxesManager;
    [SerializeField] float _fireDelay;
    [SerializeField] float _nextFire;
    [SerializeField] float _fireVelocity;

    bool gameOver = false;

    private void Start()
    {
       GameOverZone.GameOverEvent += GameOver;
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            _nextFire -= Time.deltaTime;
            if (_nextFire <= 0)
            {
                SpawnBox();
                _nextFire = _fireDelay;
            }
        }
    }

    void SpawnBox()
    {
        GameObject go= Instantiate(_boxPrefabs[Random.Range(0, _boxPrefabs.Length)], transform.position, transform.rotation);
        go.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, _fireVelocity);
        _boxesManager.FiguresList.Add(go.GetComponent<AbstractFigure>());
        scoreManager.Score += go.GetComponent<AbstractFigure>().Points;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        gameOver = true;
    }
}
