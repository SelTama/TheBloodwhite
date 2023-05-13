using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSample : MonoBehaviour
{

    private Vector3 aimPos;
    private Quaternion aimRot;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, aimPos, Time.deltaTime * 3f);
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRot, Time.deltaTime * 3f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            aimPos = transform.position + Vector3.right * 10f;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            aimRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, 90f));
        }
    }
}
