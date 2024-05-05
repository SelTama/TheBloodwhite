using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{

    public Vector4 stageDimensions;
    public GameObject playerObj;
    private Vector3 transitionPos = new Vector3 (70,0,0);
    private float offset;
    public BackgroundObjModel[] backgroundObjList;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player").gameObject;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            foreach (BackgroundObjModel bgm in backgroundObjList)
            {
                offset = transitionPos.x / bgm.distance + 0.1f * Time.deltaTime;
                bgm.Obj.transform.position = new Vector3(offset * -playerObj.transform.localPosition.x, offset * -playerObj.transform.localPosition.y, bgm.Obj.transform.localPosition.z);

            }
        }
       
    }

    [System.Serializable]
    public class BackgroundObjModel
    {
        public float distance;
        public GameObject Obj;
    }

}
