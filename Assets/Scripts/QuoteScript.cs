using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteScript : MonoBehaviour
{



    //public GameObject sliderObj;
    //public float startPos = 10f;
    //public float endPos = -10f;
    //private float slideSpeed = .04f;


    private void Awake()

    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        //sliderObj.transform.localPosition = Vector3.up * startPos;
        transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x + 5f, GameObject.FindWithTag("Player").transform.position.y, 0f);
        
    }

    private void OnDisable()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(false);
        }


        //if (endPos > sliderObj.transform.localPosition.y)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    sliderObj.transform.localPosition -= Vector3.up * slideSpeed;
        //}
    }
}
