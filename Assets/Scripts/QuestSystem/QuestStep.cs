using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep() {
        if (!isFinished)
        {
            isFinished = true;

            //Advance the quest to next step and since finished, remove this step


            Destroy(this.gameObject);
        }
    }
}
