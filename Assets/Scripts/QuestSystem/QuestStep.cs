using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }

    protected void FinishQuestStep() 
    {
        if (!isFinished)
        {

            isFinished = true;
            EventManager.instance.questEvents.AdvanceQuest(questId);

            // since finished, remove this step
            Debug.Log("XXX");
            Destroy(this.gameObject);
        }
    }
}
