using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Resources;


public class EnemyManager : MonoBehaviour
{
    ////THOU SHALT NOT MULTIIII!!
    ////if theres no enemy manager create one
    private static EnemyManager instance = null;


    public static EnemyManager Instance
    {
        get
        {
    //        //do not create new ones every time its called
            if (instance == null)
            {
                //var obj = new GameObject("BackgroundManager", typeof(EnemyManager));
                //instance = obj.GetComponent<EnemyManager>();
            }
            instance = new EnemyManager();
            return instance;
        }
    //    //do not let outside code change it
        private set { }
    }
    private EnemyManager() { }

    public void Start()
    {
        if (instance == null)
            instance = this;
    }

    public int example = 1;


}
