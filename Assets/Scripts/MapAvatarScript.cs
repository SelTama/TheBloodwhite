using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAvatarScript : MonoBehaviour
{
    public float mapMoveSpeed = 5f;
    public Vector3 target;
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //target.z = Camera.main.nearClipPlane;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, mapMoveSpeed * Time.deltaTime);
    }
}
