using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{

    private EnemyManager enemyManager;
    public string spawningUnit;


    private void Awake()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager").gameObject.GetComponent<EnemyManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //if (enemyManager.GetComponent<EnemyManager>().stageTriggers[0].currentSpawnCounts < enemyManager.GetComponent<EnemyManager>().stageTriggers[0].totalSpawnCounts)
        //{
        //    GameObject instance = Instantiate(Resources.Load(spawningUnit, typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        //    enemyManager.GetComponent<EnemyManager>().stageTriggers[0].currentSpawnCounts += 1;

        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {

    }




}
