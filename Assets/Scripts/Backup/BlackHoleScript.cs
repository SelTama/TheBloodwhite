using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject BlackholePrefab;
    public GameObject RayCastPointObj;

    public List<BlackholeModel> ItemsList = new List<BlackholeModel>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //RaycastHit2D hit = Physics2D.Raycast(RayCastPointObj.transform.position, RayCastPointObj.transform.right, Mathf.Infinity, layerMask)

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            int RaycastMask = LayerMask.GetMask("EnemyUnit");
            RaycastHit2D hit = Physics2D.Raycast(RayCastPointObj.transform.position, RayCastPointObj.transform.right, Mathf.Infinity, RaycastMask);
            if (hit.collider != null)
            {
                if (hit.collider.transform.tag == "QuafSentry")
                {
                    GameObject clone = Instantiate(BlackholePrefab, RayCastPointObj.transform.position, Quaternion.identity) as GameObject;
                    BlackholeModel bhmClone = new BlackholeModel();
                    bhmClone.BlackHoleObj = clone;
                    bhmClone.TargetPoint = hit.point;
                    bhmClone.dmg = 5;

                    ItemsList.Add(bhmClone);

                }
            }
        }
        // ust kisim sadece olusturup gonderiyor

        if (ItemsList.Count > 0)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                if(ItemsList[i].BlackHoleObj != null)
                {
                    Vector3 vector = ItemsList[i].TargetPoint - ItemsList[i].BlackHoleObj.transform.position;
                    vector = vector / vector.magnitude;
                    ItemsList[i].BlackHoleObj.transform.position += vector/1.5f;
                    if(ItemsList[i].BlackHoleObj.transform.position.x > ItemsList[i].TargetPoint.x)
                    {
                        //gameobject go =  cacheTe bir yer acan local variable (bosluk oluşturma)
                        DmgManager.instance.calc.Add(ItemsList[i].dmg);

                        GameObject go = ItemsList[i].BlackHoleObj;
                        ItemsList[i].BlackHoleObj = null;
                        Destroy(go);


                    }
                }
                else
                {
                    ItemsList.RemoveAt(i);
                }


            }

            //alt kisim process e sokuyor

        }

    }
    [System.Serializable]
    public class BlackholeModel
    {
        public GameObject BlackHoleObj;
        public Vector3 TargetPoint;
        public int dmg;

    }

}
