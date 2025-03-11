using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MapAvatarScript : MonoBehaviour
{
    public float angle;
    public float mapMoveSpeed = 20f;
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
            Vector3 direction = (target - transform.position);
            Debug.DrawRay(transform.position, direction, Color.blue);

            float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Debug.Log("Angle" + angle);
            //angle relative to z axis
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 100);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, mapMoveSpeed * Time.deltaTime);
    }
}
