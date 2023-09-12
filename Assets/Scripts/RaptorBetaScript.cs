using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorBetaScript : MonoBehaviour
{
    public delegate void LifeSignal();
    public static event LifeSignal IsAlive;
    public static event LifeSignal IsDead;
    public GameObject Mouth;

    public GameObject playerObj;
    public float tidaDistance;
    public Vector3 evadeTrajectory;
    public Vector3 evadeAreaLimitMaxY;
    public Vector3 evadeAreaLimitMinY;
    public Vector3 tidaDirection;

    public float UnitHP;
    public float AttackInterval;
    public GameObject raptorBetaBolt;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        if (IsAlive != null)
            IsAlive();

        EvadeAreaLimit();
        StartCoroutine(MainAttack());
    }

    void Update()
    {
        //OnTidaEnraged += Scream_OnTidaEnraged;
        FlankingManeuvres();
    }

    void TelekineticErasure()
    {
        UnitHP -= 20f * Time.deltaTime;

        if (UnitHP < 0)
        {
            Destroy(gameObject);
            if (IsDead != null)
                IsDead();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TelekineticErasure")
        {
            InvokeRepeating("TelekineticErasure", .1f , .1f );

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TelekineticErasure")
        {
            CancelInvoke();

        }
    }

    //private void Scream_OnTidaEnraged(object sender, EventArgs e) {
    //    Debug.Log("Space!");
    
    //}





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PsiBolt")
        {
            UnitHP = UnitHP - 5;

            if (UnitHP <= 0)
            {
                Destroy(gameObject);

                if (IsDead != null)
                    IsDead();
            }
        }

        else if (collision.gameObject.tag == "TelekineticSlash")
        {
            UnitHP = UnitHP - 30;

            if (UnitHP <= 0)
            {
                Destroy(gameObject);
                if (IsDead != null)
                    IsDead();
            }
        }
    }



    private void EvadeAreaLimit()
    {
        Camera camera = Camera.main;
        evadeAreaLimitMaxY = camera.ViewportToWorldPoint(new Vector3(0, 1.5f, 0));
        evadeAreaLimitMinY = camera.ViewportToWorldPoint(new Vector3(0, -1.5f, 0));
    }

    private void FlankingManeuvres()
    {
        Vector3 toTidaDirection = playerObj.transform.position - transform.position;
        //Vector3 fromTidaDirection = transform.position - playerObj.transform.position;

        //close in if youre too far away until 
        if (Vector3.Distance(transform.position, playerObj.transform.position) >= 14f)
        {
            Vector3 approachTrajectory = new Vector3(0, .2f, 0) + toTidaDirection;

            transform.position += (approachTrajectory * .1f * Time.deltaTime);
        }

        //do not close in more than this; if youre being in close approach, run away
        else if (Vector3.Distance(transform.position, playerObj.transform.position) <= 8f)
        {
            Vector3 evadeTrajectory = new Vector3(0, .2f, 0) - toTidaDirection;

            transform.position += evadeTrajectory * .5f * Time.deltaTime;
        }
        else
            evadeTrajectory = new Vector3(0, 0, 0);
    }

    IEnumerator MainAttack() 
    {
            float angle = Mathf.Atan2(transform.position.y, playerObj.transform.position.x) * Mathf.Rad2Deg;
            Vector3 toTidaDirection = playerObj.transform.position - transform.position;
            GameObject clone = Instantiate(raptorBetaBolt, Mouth.transform.position,  Quaternion.Euler(0,0, angle)) as GameObject;
            yield return new WaitForSeconds(AttackInterval);
        StartCoroutine(MainAttack());
    }

}
