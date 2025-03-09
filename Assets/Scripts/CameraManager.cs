using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject playerObj;
    public float rightLimit;
    public float leftLimit;
    public float upLimit;
    public float downLimit;
    public float cameraDistance = 70f;
     
    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MovementBoundaries();
    }


    private void LateUpdate()
    {
        CameraFollowCruiseMode();
    }

    private void MovementBoundaries()
    {
        Camera camObj = Camera.main;
        leftLimit = camObj.ViewportToWorldPoint(new Vector3(.3f, 0, Camera.main.nearClipPlane)).x;
        rightLimit = camObj.ViewportToWorldPoint(new Vector3(.7f, 0, Camera.main.nearClipPlane)).x;
        upLimit = camObj.ViewportToWorldPoint(new Vector3(0, .9f, Camera.main.nearClipPlane)).y;
        downLimit = camObj.ViewportToWorldPoint(new Vector3(0, .1f, Camera.main.nearClipPlane)).y;

    }


    private void CameraFollowCruiseMode()
    {

        //Camera.main.transform.position = playerObj.transform.position - new Vector3(0, 0, cameraDistance);
        Camera.main.nearClipPlane = MathF.Abs(transform.position.z) - 0.1f;

        //move right
        if (playerObj.transform.position.x >= rightLimit - .5f)
        {
            Camera.main.transform.position += Vector3.right * 6f * Time.deltaTime;

            if (Input.GetKey(KeyCode.W) && playerObj.transform.position.y >= upLimit - .5f)
            {
                Camera.main.transform.position += Vector3.up * 6f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S) && playerObj.transform.position.y <= downLimit + .5f)
            {
                Camera.main.transform.position += Vector3.down * 6f * Time.deltaTime;
            }
        }

        //move left
        else if (playerObj.transform.position.x <= leftLimit + .5f)  
        {
            Camera.main.transform.position += Vector3.left * 6f * Time.deltaTime;

            if (Input.GetKey(KeyCode.W) && playerObj.transform.position.y >= upLimit - .5f)
            {
                Camera.main.transform.position += Vector3.up * 6f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S) && playerObj.transform.position.y <= downLimit + .5f)
            {
                Camera.main.transform.position += Vector3.down * 6f * Time.deltaTime;
            }
        }

        //move up
        else if (playerObj.transform.position.y >= upLimit - .5f)
        {
            Camera.main.transform.position += Vector3.up * 6f * Time.deltaTime;

            if (Input.GetKey(KeyCode.D) && playerObj.transform.position.x >= rightLimit - .5f)
            {
                Camera.main.transform.position += Vector3.right * 6f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A) && playerObj.transform.position.x <= leftLimit + .5f)
            {
                Camera.main.transform.position += Vector3.left * 6f * Time.deltaTime;
            }
        }

        //move down
        else if (playerObj.transform.position.y <= downLimit + .5f)
        {
            Camera.main.transform.position += Vector3.down * 6f * Time.deltaTime;

            if (Input.GetKey(KeyCode.D) && playerObj.transform.position.x >= rightLimit - .5f)
            {
                Camera.main.transform.position += Vector3.right * 6f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A) && playerObj.transform.position.x <= leftLimit + .5f)
            {
                Camera.main.transform.position += Vector3.left * 6f * Time.deltaTime;
            }
        }

        else
        {
            Camera.main.transform.position = Camera.main.transform.position;
        }

    }


}
