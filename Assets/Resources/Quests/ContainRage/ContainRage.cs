using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainRage : QuestStep
{
    public GameObject playerObj;
    public int fury;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        fury =  ((int)playerObj.GetComponent<TidaStateScript>().tidaFury);
        TooMuchFury();
    }


    private void TooMuchFury() 
    {

        if (playerObj.GetComponent<TidaStateScript>().tidaFury >= 10)
        {
            FinishQuestStep();
            Debug.Log("ZZZ");
        }
    
    }

}
