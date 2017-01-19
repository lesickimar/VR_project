using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {

    public delegate void newFly();
    public static event newFly NewFly;

    public static Vector3 _position;
    private bool gazeAt = false;
    public float gazeTime;
    private void Start()
    {
        _position = gameObject.transform.position;
    }

    void Update()
    {
        if (gazeAt)
        {
            gazeTime += Time.deltaTime;

            if (NewFly != null && gazeTime >= 2)
            {
                NewFly();
                Debug.Log("DUOA");
                gazeAt = false;
            }
        }
    }

    //public void PointerEnter()
    //{
    //    gazeAt = true;
    //}

    public void PointerClick()
    {
        Input.GetButtonDown("Jump");
        GameCore.instance._fly = true;
    }

}
