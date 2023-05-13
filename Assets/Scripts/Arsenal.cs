using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    public bool isActive = false;

    public ArmourModel[] armourObjList;

    void Update()
    {

        if (isActive)
        {
            foreach (ArmourModel armour in armourObjList)
            {
                Debug.Log("red");

                //armour.skin.GetComponent<SpriteRenderer>().color = Color.red;

                //armour.armourMaterial.SetFloat("1EnergyScale", 5f);
                //armour.armourMaterial.SetColor("1EnergyColour", Color.red);
                //armour.skin.GetComponent<SpriteRenderer>().material.SetColor("1EnergyColour", Color.red);
                armour.skin.GetComponent<SpriteRenderer>().material = armour.armourMaterialEnraged;

            }
        }
        
    }

    [System.Serializable]
    public class ArmourModel
    {
        public GameObject skin;
        public Material armourMaterial;
        public Material armourMaterialEnraged;
    }
}
