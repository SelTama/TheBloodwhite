using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgManager : MonoBehaviour
{
    public static DmgManager instance;
    public List<int> calc = new List<int>();
        

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
