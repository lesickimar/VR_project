using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEyes : MonoBehaviour {

    private float timer;
    private bool start;
    public float speed = .002f;
    private float up = .2f;

    GvrHead gviewer;
    public GameObject gvrviewer;
	// Use this for initialization
	void Start ()
    {
        gviewer = gvrviewer.GetComponent<GvrHead>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (start)
        {
            timer += Time.deltaTime;
           // Debug.Log(timer);
            if(timer >= 2)
            {
                OpenYourEyes();

            }
        }	

        if (timer >= 9.2)
        {
            Destroy(this.gameObject);
            gviewer = gvrviewer.GetComponent<GvrHead>();
            gviewer.trackRotation = true;
            GameCore.instance.isMoving = true;
            Debug.Log(gviewer.trackRotation);
            
        }
	}

    public void GameStart()
    {
        start = true;
    }

    public void OpenYourEyes()
    {
        transform.Translate(0, up * Time.deltaTime, 0, Space.Self);
        
    }
}
