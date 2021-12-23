using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour {

    [SerializeField] BoxesManager boxesManager;
    [SerializeField] GameOverZone[] gameOverZone;
    [SerializeField] BoxLauncher[] boxLaunchers;

    float[] distanceBetweenZonesAndCamera;
    float[] distancesBetweenLaunchersAndCamera;

    private void Start()
    {
        CalculateDistanceBetweenLaunchersAndCamera();
        CalculateDistanceBetweenZonesAndCamera();
    }

    void CalculateDistanceBetweenLaunchersAndCamera()
    {
        distancesBetweenLaunchersAndCamera = new float[boxLaunchers.Length];
        for (int i = 0; i < distancesBetweenLaunchersAndCamera.Length; i++)
        {
            distancesBetweenLaunchersAndCamera[i] = Mathf.Abs(this.transform.position.y - boxLaunchers[i].transform.position.y);
        }
    }

    void CalculateDistanceBetweenZonesAndCamera()
    {
        distanceBetweenZonesAndCamera = new float[gameOverZone.Length];
        for (int i = 0; i < distanceBetweenZonesAndCamera.Length; i++)
        {
            distanceBetweenZonesAndCamera[i] = Mathf.Abs(this.transform.position.y - gameOverZone[i].transform.position.y);
        }
    }


    public void MoveCamera(Vector3 targetPosition)
    {
        if ((gameObject.transform.position.y < targetPosition.y))
        {
            StartCoroutine(MovingCamera(targetPosition));
            boxesManager.CheckBoxes();
        }
    }
    
    IEnumerator MovingCamera(Vector3 targetPosition)
    {
        while(gameObject.transform.position.y < targetPosition.y - 1)
        {
            float positionY = Mathf.Lerp(gameObject.transform.position.y, targetPosition.y, 1 * Time.deltaTime);
            gameObject.transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
            yield return null;
        }
        ChangeDistnaces();      
    }

    void ChangeDistnaces()
    {
        for (int i = 0; i < gameOverZone.Length; i++)
        {
            gameOverZone[i].transform.position = new Vector3(gameOverZone[i].transform.position.x, this.gameObject.transform.position.y -  distanceBetweenZonesAndCamera[i], gameOverZone[i].transform.position.z);
        }

        for (int i = 0; i < boxLaunchers.Length; i++)
        {
            boxLaunchers[i].transform.position = new Vector3(boxLaunchers[i].transform.position.x, this.gameObject.transform.position.y - distancesBetweenLaunchersAndCamera[i], boxLaunchers[i].transform.position.z);
        }
    }


}
