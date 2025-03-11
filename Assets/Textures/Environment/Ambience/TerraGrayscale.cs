using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TerraGrayscale : MonoBehaviour
{
    private GameObject playerObj;

    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Volume>().weight = 1.7f -  playerObj.transform.position.x / 80f ;
    }
}
