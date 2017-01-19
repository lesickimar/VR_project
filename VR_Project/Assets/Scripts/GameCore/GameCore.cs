using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {
    //---Singleton class---//
    public static GameCore instance;
    //---------------------//

    public bool _fly;
    public bool isMoving;

    

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
            instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}

    }




	


   

}
