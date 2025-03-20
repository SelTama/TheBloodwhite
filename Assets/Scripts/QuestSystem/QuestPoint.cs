using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]

    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Quest")]

    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    public GameObject button;

    private bool playerIsNear = false;

    private string questId;

    private QuestState currentQuestState;

    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
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
            button.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {

                if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
                {
                    EventManager.instance.questEvents.StartQuest(questId);
                }
                else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
                {
                    EventManager.instance.questEvents.FinishQuest(questId);
                }
            }          

        }
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
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
