using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuotesManager : MonoBehaviour
{

    public GameObject playerObj;
    public bool NPCCollision = false;
    public int CollisionNPC;
    public GameObject FButton;
    private int _currentDialogueIndex = 0;


    public List<RuntimeDialogueGraph> dialogueGraphs = new List<RuntimeDialogueGraph>();


    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player").gameObject;
        FButton.SetActive(false);
    }


    
    void Update()
    {

        if (NPCCollision)
        {
            FButton.SetActive(true);
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                DialogueManager manager = FindObjectOfType<DialogueManager>();
                if (manager != null && dialogueGraphs.Count > 0)
                {
                    manager.StartDialogue(dialogueGraphs[_currentDialogueIndex]);
                }



            }
        }
        else
        {
            FButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NPCCollision = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NPCCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            NPCCollision = false;
        }
    }

    public void SetDialogue(int index)
    {
        if (index >= 0 && index < dialogueGraphs.Count)
            _currentDialogueIndex = index;
    }


    [System.Serializable]
    public class QuoteModel
    {
        public RuntimeDialogueGraph RuntimeDialogue;        
    }


}