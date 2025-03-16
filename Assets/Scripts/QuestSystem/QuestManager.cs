using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;


    private void Awake()
    {
        questMap = CreateQuestMap();

        //TEST
        //Quest quest = GetQuestById("SpeakToZaviseleth");
        //Debug.Log(quest.info.displayName);
        //Debug.Log(quest.state);
        //Debug.Log(quest.CurrentStepExists());
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


    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Load all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests
        QuestInfoSO[] allquests = Resources.LoadAll<QuestInfoSO>("Quests");
        //CreateQuestMap
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allquests)
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



    private void StartQuest(string id) 
    {
        //TODO - start the quest

        Debug.Log("Start Quest" + id);
    
    }
    private void AdvanceQuest(string id)
    {
        //TODO - advance the quest

        Debug.Log("Advance Quest" + id);

    }
    private void FinishQuest(string id)
    {
        //TODO - finish the quest

        Debug.Log("Finish Quest" + id);

    }













}
