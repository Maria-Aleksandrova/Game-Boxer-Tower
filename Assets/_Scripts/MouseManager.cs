using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {


    [SerializeField] LineRenderer dragLine;
    [SerializeField] float _velocityRatio;
    [SerializeField] bool useSpring;

    Rigidbody2D _grabbedObject;
    SpringJoint2D _springJoin;  

	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit)
            {
                if(hit.collider.GetComponent<Rigidbody2D>())
                {
                    GrabObject(hit);                   
                }
            }
        }

        if(Input.GetMouseButtonUp(0) && _grabbedObject)
        {
            DropObject();         
        }
	}

    void GrabObject(RaycastHit2D hit)
    {
        _grabbedObject = hit.collider.GetComponent<Rigidbody2D>();
        if (useSpring)
        {
            SetSpringJoint(hit);
        }
        else
        {
            _grabbedObject.gravityScale = 0;
        }
        dragLine.enabled = true;
    }

    void SetSpringJoint(RaycastHit2D hit)
    {        
        _springJoin = _grabbedObject.gameObject.AddComponent<SpringJoint2D>();
        _springJoin.anchor = _grabbedObject.transform.InverseTransformPoint(hit.point);
        _springJoin.connectedAnchor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _springJoin = hit.collider.GetComponent<AbstractFigure>().SetSpringJoint2D(_springJoin);
        _springJoin.connectedBody = null;
    }

    void DropObject()
    {
        if (useSpring)
        {
            Destroy(_springJoin);
        }
        else
        {
            _grabbedObject.gravityScale = 1;
        }
        _grabbedObject = null;
        dragLine.enabled = false;
    }

    private void FixedUpdate()
    {
        if(_grabbedObject)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (useSpring)
            {               
                _springJoin.connectedAnchor = mousePos;
            }
            else
            {
                _grabbedObject.velocity = (mousePos - _grabbedObject.position) * _velocityRatio;
            }
        }
    }

    private void LateUpdate()
    {
        if (_grabbedObject)
        {
            if (useSpring)
            {
                Vector2 worldAnchor = _grabbedObject.transform.TransformPoint(_springJoin.anchor);
                dragLine.SetPosition(0, new Vector3(worldAnchor.x, worldAnchor.y, -1));
                dragLine.SetPosition(1, new Vector3(_springJoin.connectedAnchor.x, _springJoin.connectedAnchor.y, -1));             
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragLine.SetPosition(0, new Vector3(_grabbedObject.transform.position.x, _grabbedObject.transform.position.y, -1));
                dragLine.SetPosition(1, new Vector3(mousePos.x,mousePos.y, -1));
            }
        }
    }
}
