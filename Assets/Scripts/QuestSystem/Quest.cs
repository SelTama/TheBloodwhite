using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    //static info
    public QuestInfoSO info;

    //state info

    public QuestState state;

    private int currentQuestStepIndex;

    //default quest settings
    public Quest(QuestInfoSO questInfo) 
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;

    }

    public void MoveToNextStep() 
    {
        currentQuestStepIndex++;    
    }

    //make sure the number of quest steps do no overshoot accidentally
    //check if theres a next step 
    public bool CurrentStepExists() 
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);    
    }


    //create the quest prefab under the Quest prefab
    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id);
        }
    }


    private GameObject GetCurrentQuestStepPrefab() 
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("tried to get quest step prefab but stepIndex was out of range indicating that"
                + "theres no current step: QuestId=" + info.id + ",step index=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    
    }


}
