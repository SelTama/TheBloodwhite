using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaScript : MonoBehaviour
{
    public delegate void TriggerIsActivated();
    public static event TriggerIsActivated IsClosed;
    public static event TriggerIsActivated IsOpen;
    public GameObject playerObj;
    
    [SerializeField] bool isActivated = false;
    [SerializeField] int currentSpawnCounts;
    [SerializeField] int totalSpawnCounts;
    [SerializeField] int enemyLifeSignal;
    [SerializeField] int enemyDeathSignal;


    [SerializeField] List<TriggerArea> triggerAreaItems = new List<TriggerArea>();

    [System.Serializable]
    public class TriggerArea
    {
        public Vector3 spawnCoordinate;
        public string spawningUnit;
    }

    private void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        if (isActivated == true)
        {
            transform.position = playerObj.transform.position + new Vector3(10, 0, 0);
        }


        if (isActivated == true && enemyLifeSignal == 0)
        {
            foreach (var spawnPoint in triggerAreaItems)
            {
                //open the spawnpoints  
                if (currentSpawnCounts < totalSpawnCounts)
                {
                    GameObject instance = Instantiate(Resources.Load("Prefabs/EnemyUnits/" + spawnPoint.spawningUnit, typeof(GameObject)), transform.position + spawnPoint.spawnCoordinate, Quaternion.identity) as GameObject;
                    currentSpawnCounts += 1;

                }
            }
        }

        if (isActivated && enemyDeathSignal == totalSpawnCounts)
        {
            CloseTriggerArea();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isActivated = true;

            if (IsOpen != null)
                IsOpen();
        }
    }

    private void OnEnable()
    {        
        RaptorBetaScript.IsAlive += SenseLifeSignal;
        RaptorBetaScript.IsDead += RemoveLifeSignal;
        RaptorBetaScript.IsDead += CountTheTotalDeadOfTheTrigger;
    }
    private void OnDisable()
    {
        RaptorBetaScript.IsAlive += SenseLifeSignal;
        RaptorBetaScript.IsDead -= RemoveLifeSignal;
    }
    void SenseLifeSignal()
    {
        if (isActivated)
        {
            enemyLifeSignal = enemyLifeSignal + 1;
        }
    }

    void RemoveLifeSignal()
    {
        if (isActivated)
        {
            enemyLifeSignal = enemyLifeSignal - 1;
        }
    }
    void CountTheTotalDeadOfTheTrigger() 
    {
        if (isActivated)
        {            
            enemyDeathSignal = enemyDeathSignal + 1;
        }
    }
    void CloseTriggerArea() 
    {
        if (IsClosed != null)
            IsClosed();

        Destroy(gameObject);
    }


}
