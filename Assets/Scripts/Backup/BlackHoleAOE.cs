using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAOE : MonoBehaviour
{
    public GameObject RayCastPointObj;
    public GameObject BlackHoleAOEPrefab;
    public GameObject bolt;
    public Vector3 momentum = new Vector3(10,0,0);
    private float availableRange = 30f;
    private Vector3 callRange = new Vector3(10, 0, 0);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int RaycastMask = LayerMask.GetMask("EnemyUnit");
            RaycastHit2D hit = Physics2D.Raycast(RayCastPointObj.transform.position, RayCastPointObj.transform.right, availableRange, RaycastMask);
            if(hit.collider != null)
            {
                GameObject clone = Instantiate(BlackHoleAOEPrefab, RayCastPointObj.transform.position + callRange, Quaternion.identity) as GameObject;
            }
        }
                                    
    }

    
}
