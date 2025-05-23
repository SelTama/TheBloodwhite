﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButtonController : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject Button;
    public bool PlayerCollision = false;

    // Update is called once per frame


    private void Start()
    {
        playerObj = GameObject.FindWithTag("Player").gameObject;
        Button.SetActive(false);
    }

    void Update()
    {

        if (PlayerCollision)
        {
            Button.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                playerObj.GetComponent<PlayerController>().stageDimensions = GameObject.FindWithTag("BG").GetComponent<BackgroundMovement>().stageDimensions;
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
        else
        {
            Button.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCollision = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCollision = false;
        }
    }
}
