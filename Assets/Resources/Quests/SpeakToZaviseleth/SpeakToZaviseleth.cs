using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakToZaviseleth : QuestStep
{
    public bool speakToZavi = false;
    public bool speakToSeleth = false;


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

        if (speakToZavi && speakToSeleth)
        {
            FinishQuestStep();
        }
    }

    private void ZaviIsSpoken()
    {
        speakToZavi = true;
        

        if (speakToZavi && speakToSeleth)
        {
            FinishQuestStep();
        }
    }    
    

}
