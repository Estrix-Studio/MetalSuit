using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    public GameObject objectToDrag;
    public GameObject ObjectDragToPos;

    public float Dropdistance;
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
        float Distance = Vector3.Distance(objectToDrag.transform.position, ObjectDragToPos.transform.position);
        if(Distance < Dropdistance) 
        {
            isLocked = true;
            objectToDrag.transform.position = ObjectDragToPos.transform.position;
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}
