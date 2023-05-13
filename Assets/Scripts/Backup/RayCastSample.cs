using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSample : MonoBehaviour
{
    public GameObject RayCastPointObj;
    public GameObject BlackHole;

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            int layerMask = LayerMask.GetMask("EnemyUnit");
            RaycastHit2D hit = Physics2D.Raycast(RayCastPointObj.transform.position, RayCastPointObj.transform.right, Mathf.Infinity, layerMask);// 1. degisken çıkış noktası ikinci değişken direction vektoru 
            if (hit.collider != null)
            {
                if (hit.transform.tag == "QuafSentry")
                {
                
                    GameObject clone = Instantiate(BlackHole, RayCastPointObj.transform.position, RayCastPointObj.transform.localRotation) as GameObject;
                    hit.transform.gameObject.GetComponent<EnemyScript>().StatsList[0].UnitHP -= 5f;

                    //hit.point dersek çarptiği nokta ama hit.transform.position denirse objenin positioni olan merkez nokta
                    //hit.transform.gameObject.GetComponent<>..... carptigin seyin değişkenlerine ulaşmak / fonksiyon tetikleme / değiştirecek şey
                    print(hit.transform.name);
                    Debug.LogError("sayibudur: " + Singleton.Instance.sample);

                }
            }
        }
        

    }
}
