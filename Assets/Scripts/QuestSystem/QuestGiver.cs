using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestGiver : MonoBehaviour
{
    public GameObject playerObj;

    [Header("Quest")]

    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Quest")]

    //[SerializeField] private bool startPoint = true;
    //[SerializeField] private bool finishPoint = true;

    public GameObject questStateIndicator;
    public GameObject questInPosession;

    public bool playerIsNear = false;

    public GameObject button;

    private string questId;

    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        playerObj = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        SubmitPressed();
    }

    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }
 
        if (playerIsNear)
        {            
            button.SetActive(true);

            if (Input.GetKeyUp(KeyCode.F))
            {
                if (transform.childCount < 4)
                {
                    Instantiate(questInPosession, this.transform);

                }






            }
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
