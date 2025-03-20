using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakToZaviseleth : QuestStep
{
    [Header("SpeakToZaviseleth")]

    [Header("Objectives")]

    private bool speakToZavi = false;
    private bool speakToSeleth = false;

    public bool questInProgress = false;
    public bool questComplete = false;

    private void Start()
    {
        questInProgress = false;
    }

    //private void Update()
    //{
    //    CompleteQuest();
    //}


    private void OnEnable()
    {
        Zavi.ZaviIsInteracted += ZaviIsSpoken;
        Seleth.SelethIsInteracted += SelethIsSpoken;
    }

    private void OnDisable()
    {
        Zavi.ZaviIsInteracted -= ZaviIsSpoken;
        Seleth.SelethIsInteracted -= SelethIsSpoken;
    }

    private void SelethIsSpoken()
    {
        speakToSeleth = true;
        Debug.Log("spoke to seleth");
        if (speakToZavi)
        {
            questComplete = true;
            FinishQuestStep();
        }     
    }

    private void ZaviIsSpoken()
    {
        speakToZavi = true;
        Debug.Log("spoke to zavi");
        if (speakToSeleth)
        {
            questComplete = true;
            FinishQuestStep();
        }

    }

    //private void CompleteQuest() 
    //{
    //    if (questComplete)
    //    {
    //        FinishQuestStep();
    //        //FinishTheQuest();
    //    }    
    //}
    ////private void FinishTheQuest() 
    ////{

    ////    Destroy(this.gameObject);
    //}
}
