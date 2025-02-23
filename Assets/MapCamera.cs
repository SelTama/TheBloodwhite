using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public float mapCamMoveSpeed;
    public Vector3 target;
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {        
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x >= .87f)
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            target.z = transform.position.z;
            mapCamMoveSpeed =  (Camera.main.ScreenToViewportPoint(Input.mousePosition).x -.87f) * 150f;
            transform.position = Vector3.MoveTowards(transform.position, target, Mathf.Clamp(mapCamMoveSpeed, 0, 19.5f) * Time.deltaTime);
        }
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).y >= .9f)
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            target.z = transform.position.z;
            mapCamMoveSpeed = (Camera.main.ScreenToViewportPoint(Input.mousePosition).y - .9f) * 100f;
            transform.position = Vector3.MoveTowards(transform.position, target, Mathf.Clamp(mapCamMoveSpeed, 0, 10f) * Time.deltaTime);
        }
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x <= .13f)
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            target.z = transform.position.z;
            mapCamMoveSpeed = (0.13f - Camera.main.ScreenToViewportPoint(Input.mousePosition).x) * 150f;
            transform.position = Vector3.MoveTowards(transform.position, target, Mathf.Clamp( mapCamMoveSpeed, 0, 19.5f) * Time.deltaTime);
        }
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).y <= .1f)
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            target.z = transform.position.z;
            mapCamMoveSpeed = (0.1f - Camera.main.ScreenToViewportPoint(Input.mousePosition).y) * 100f;
            transform.position = Vector3.MoveTowards(transform.position, target, Mathf.Clamp(mapCamMoveSpeed, 0, 10f) * Time.deltaTime);
        }
        else
        {
            target = transform.position;

        }
    }
}
