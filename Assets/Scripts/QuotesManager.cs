using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuotesManager : MonoBehaviour
{

    public GameObject playerObj;
    public bool NPCCollision = false;
    public int CollisionNPC;

    public List<QuoteModel> QuotesList = new List<QuoteModel>();


    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player").gameObject;
        QuotesList[CollisionNPC].FButton.SetActive(false);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {           
               
        if (NPCCollision)
        {
            QuotesList[CollisionNPC].FButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                QuotesList[CollisionNPC].NPCQuote.SetActive(true);

            }
        }
        else
        {
            QuotesList[CollisionNPC].FButton.SetActive(false);
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
    

    [System.Serializable]
    public class QuoteModel
    {
        public GameObject NPCQuote;
        public GameObject FButton;
    }


}