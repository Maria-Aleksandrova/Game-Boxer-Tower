using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Linq;

public class BoxesManager : MonoBehaviour {

    [Tooltip ("Destroy boxes below this distance")]
    [SerializeField] float distnaceBelowCameraView;

    List<AbstractFigure> _figuresList;
    List<AbstractFigure> _legacyFiguresList;

    public List<AbstractFigure> FiguresList { get { return _figuresList; } }

    void Start () {
        _figuresList = new List<AbstractFigure>();
        _legacyFiguresList = new List<AbstractFigure>();
    }

    public void CheckBoxes()
    {
        CheckVisibleBoxes();
        CheckUnvisibleBoxes();        
    }

    void CheckVisibleBoxes()
    {
        for (int i = 0; i < _figuresList.Count; i++)
        {
            if (IsInCameraView(_figuresList[i]))
            {
                _figuresList.RemoveAt(i);
                i--;
            }
        }
    }

    void CheckUnvisibleBoxes()
    {
        if (_legacyFiguresList.Count > 0)
        {
            if (Camera.main.WorldToViewportPoint(_legacyFiguresList[0].transform.position).y < distnaceBelowCameraView)
            {
                for (int i = 0; i < _legacyFiguresList.Count; i++)
                {
                    if (Camera.main.WorldToViewportPoint(_legacyFiguresList[i].transform.position).y < distnaceBelowCameraView)
                    {
                        Destroy(_legacyFiguresList[i].gameObject);
                        _legacyFiguresList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    bool IsInCameraView(AbstractFigure figure)
    {
        Vector3 boxPosition = Camera.main.WorldToViewportPoint(figure.transform.position); //if value x or y is bigger than 1 or less than 0 then this object is out of camera view 
        if (boxPosition.y < 0)
        {
            _legacyFiguresList.Add(figure);
            Destroy(figure.GetComponent<Rigidbody2D>());
            return true;
        }
        return false;
    }

}
