using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CubeScript : MonoBehaviour {

    private float timer;
    private bool gazeAt;
    public float gazeTime = 2f;

    private bool dupa = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gazeAt)
        {
            
            timer += Time.deltaTime;
            
            if(timer >= gazeTime)
            {

                
                //ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0;
            }
        }


	}

    public void PointerEnter()
    {
        gazeAt = true;
    }

    public void PointerExit()
    {
        gazeAt = false;
        timer = 0;
    }

    public void PointerClick()
    {
        GameCore.instance._fly = true;
        //GameCore.instance.isMoving = false;
    }

    public void DUPAAAAClickEvent()
    {
        Debug.Log("Dupa");
    }

    public void Move()
    {
        
    }
    //if (_rotate) rotacja
    //    {
    //        transform.Rotate(2, 1, 0, Space.Self);

    //    }
    //    else
    //        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime);
}
