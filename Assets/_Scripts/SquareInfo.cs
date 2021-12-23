using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareInfo : AbstractFigure {

    [ContextMenu("CheckVisible()")]
    void CheckVisible()
    {
        Vector3 boxPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        Debug.Log(boxPosition);
        if (boxPosition.x > 1 || boxPosition.y > 1)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

}
