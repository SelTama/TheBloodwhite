using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAvatarScript : MonoBehaviour
{
    private float mapMoveSpeed = 5f;
    private Vector3 target;
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0f;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, mapMoveSpeed * Time.deltaTime);
    }
}
