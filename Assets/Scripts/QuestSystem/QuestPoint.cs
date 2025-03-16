using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]

    [SerializeField] private QuestInfoSO questInfoForPoint;


    private bool playerIsNear = false;

    private string questId;

    private QuestState currentQuestState;


    private void Awake()
    {
        questId = questInfoForPoint.id;
    }
    private void Update()
    {
        SubmitPressed();
    }

    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;   
    }

    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }


        if (playerIsNear && Input.GetKeyDown(KeyCode.F))
        {
            EventManager.instance.questEvents.StartQuest(questId);
            EventManager.instance.questEvents.AdvanceQuest(questId);
            EventManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("quest with id : " + questId + "update to state: " + currentQuestState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsNear = false;
        }
    }



}
