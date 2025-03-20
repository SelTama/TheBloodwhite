using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject playerObj;
    private Dictionary<string, Quest> questMap;

    //quest start req
    private bool isTidaSane;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        questMap = CreateQuestMap();
    }
    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Load all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        //CreateQuestMap
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map" + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map" + id);
        }
        return quest;
    }


    private void OnEnable()
    {
        EventManager.instance.questEvents.onStartQuest += StartQuest;
        EventManager.instance.questEvents.onStartQuest += AdvanceQuest; 
        EventManager.instance.questEvents.onStartQuest += FinishQuest;
    }
    private void OnDisable()
    {
        EventManager.instance.questEvents.onStartQuest -= StartQuest;
        EventManager.instance.questEvents.onStartQuest -= AdvanceQuest;
        EventManager.instance.questEvents.onStartQuest -= FinishQuest;
    }

    private void Start()
    {
        //broadcast the initial state of all quests
        foreach (Quest quest in questMap.Values)
        {
            EventManager.instance.questEvents.QuestStateChange(quest);
        }
    }

   
    // any time you want to update the state
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        EventManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        // check if Tida is sane

        if (playerObj.GetComponent<TidaStateScript>().tidaMadnessLevel <= 1000)
        {
            isTidaSane = true;

            if (!isTidaSane)
            {
                meetsRequirements = false;
            }
            // since we've proven meetsRequirements to be false at this point.
        }

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {

            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
                // add this break statement here so that we don't continue on to the next quest,
                // since we've proven meetsRequirements to be false at this point.
                break;
            }
        }
        return meetsRequirements;

    }
    private void Update()
    {
        // Loop through ALL quests
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);

        Debug.Log("start Quest" + id);

    }
    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        //if there are no more steps, then we've finished all steps
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
        Debug.Log("Advance Quest" + id);

    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);

        Debug.Log("Finish Quest" + id);
    }

    private void ClaimRewards(Quest quest)
    {
        playerObj.GetComponent<TidaStateScript>().tidaMadnessLevel = 100;
    }



}
