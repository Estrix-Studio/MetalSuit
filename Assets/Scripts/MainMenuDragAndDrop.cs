using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDragAndDrop : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject ObjectDragtoPos;

    public float DropDistance;

    public bool isLocked;

    Vector2 objectInitPos;

    // Start is called before the first frame update
    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
    }

    public void DragObject()
    {
        if(!isLocked) 
        {
            objectToDrag.transform.position = Input.mousePosition;

        }
    }
    public void DropObject()
    {
        float Distance =  Vector3.Distance(objectToDrag.transform.position, ObjectDragtoPos.transform.position);
        if(Distance < DropDistance )
        {
            isLocked = true;
            objectToDrag.transform.position = ObjectDragtoPos.transform.position;
        }else
        {
            objectToDrag.transform.position = objectInitPos;

        }
    }
}
