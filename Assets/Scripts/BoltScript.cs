using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public GameObject hitAnimation;
    void Update()
    {
        Destroy(gameObject, 2f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.tag == "EnemyUnit")
        {
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "RaptorBetaBolt")
        {
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
