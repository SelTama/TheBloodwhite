using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Start is called before the first frame update
    public static Singleton Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int sample = 5;


}
