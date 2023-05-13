using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public delegate void LifeSignal();
    public static event LifeSignal IsAlive;
    public static event LifeSignal IsDead;

    public GameObject playerObj;
    public float tidaDistance;
    public Vector3 evadeTrajectory;
    public Vector3 evadeAreaLimitMaxY;
    public Vector3 evadeAreaLimitMinY;
    public Vector3 tidaDirection;

    public List<StatsModel> StatsList = new List<StatsModel>();

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        if (IsAlive != null)
            IsAlive();

        EvadeAreaLimit();
    }

    void Update()
    {
        FlankingManeuvres();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TelekineticErasure")
        {
            InvokeRepeating("TelekineticErasure", 0.1f, 0.1f);

            if (StatsList[0].UnitHP <= 0)
            {
                Destroy(gameObject);
                if (IsDead != null)
                    IsDead();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TelekineticErasure")
        {
            CancelInvoke();

            StatsList[0].UnitHP = StatsList[0].UnitHP - 5f;

            if (StatsList[0].UnitHP < 0)
            {
                Destroy(gameObject);
                if (IsDead != null)
                    IsDead();
            }
        }
    }


    void TelekineticErasure()
    {
        StatsList[0].UnitHP -= 0.1f;

        if (StatsList[0].UnitHP < 0)
        {
            Destroy(gameObject);
            if (IsDead != null)
                IsDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PsiBolt" )
        {
            StatsList[0].UnitHP = StatsList[0].UnitHP - 5f;

            Debug.Log("took a psi bolt to the knee");

            if (StatsList[0].UnitHP <= 0)
            {

                Debug.Log("took a DEATH BLOW to the knee");
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
        //else if (transform.position.y <= playerObj.transform.position.y + 2f && transform.position.y >= playerObj.transform.position.y + 6f)
        //{
        //    Vector3 gainAltitudeEvadeTrajectory = Vector2.Perpendicular(fromTidaDirection);

        //    transform.position += gainAltitudeEvadeTrajectory * .3f * Time.deltaTime;
        //}
        //else if (transform.position.y >= playerObj.transform.position.y - 2f && transform.position.y <= playerObj.transform.position.y - 6f)
        //{
        //    Vector3 dropAltitudeEvadeTrajectory = Vector2.Perpendicular(toTidaDirection);

        //    transform.position += dropAltitudeEvadeTrajectory * .3f * Time.deltaTime;
        //}

        else
            evadeTrajectory = new Vector3(0, 0, 0);

    }

    [System.Serializable]
    public class StatsModel
    {
        public float UnitHP;
    }


}

