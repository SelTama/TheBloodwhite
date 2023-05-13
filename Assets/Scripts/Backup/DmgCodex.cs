using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCodex : MonoBehaviour
{
    ////THOU SHALT NOT MULTIIII!!
    ////if theres none create one
    private static DmgCodex instance = null;
    public static DmgCodex Instance
    {
        get
        {
            //        //do not create new ones every time its called
            if (instance == null)
            {
                var obj = new GameObject("BackgroundManager", typeof(DmgCodex));
                instance = obj.GetComponent<DmgCodex>();
            }
            return instance;
        }
        //    //do not let outside code change it
        private set { }
    }
    private DmgCodex() { }

    public void Start()
    {
        if (instance == null)
            instance = this;
    }

    public int RaptorBetaBoltDmg = 5;


}
