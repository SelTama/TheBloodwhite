using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TidaStateScript : MonoBehaviour
{


    public event EventHandler OnTidaEnraged;


    public ArmourModel[] armourObjList;

    //TIDA MENTAL STATUS

    public float tidaFury;
    public float tidaFuryMultiplier;
    public float tidaFuryLimit;
    public float tidaEnrageDuration;
    public float tidaMadnessLevel;
    public float tidaMadnessThreshhold;
    public bool tidaIsInCombat;
    public bool tidaIsEnraged;


    //TRIGGERED EVENTS

    public GameObject activeTrigger;
    //public GameObject enragedAnim;
    public AudioClip combatModeMusic;
    public AudioClip cruiseModeMusic;


    public AudioClip GetCruiseModeMusic()
    {
        return cruiseModeMusic;
    }


    public AudioClip GetCombatModeMusic()
    {
        return combatModeMusic;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "RaptorBetaBolt" && !tidaIsEnraged)
        {
            tidaFury = tidaFury + collision.gameObject.GetComponent<RaptorBetaBoltScript>().DMG;
            CheckTidaFuryLevel();
            if (collision.gameObject.tag == "RaptorBetaBolt" && tidaIsEnraged)
            {
                tidaFury = tidaFuryLimit;
            }
        }
    }

    private void TidaEngagesCombat(){
        tidaIsInCombat = true;
    }
    private void TidaFinishedCombat(){
        tidaIsInCombat = false;
    }
    private void TEIsOnCooldown(){
        StartCoroutine(TelekineticErasureCooldown());
    }

    //private void TSIsOnCooldown(){
    //    StartCoroutine(TelekineticSlashCooldown());
    //}


    private void CheckTidaFuryLevel()
    {
        if (tidaFury >= tidaFuryLimit)
        {
            tidaIsEnraged = true;
            StartCoroutine(TidaEnraged());
            Debug.Log("OH F TIDA IS BUCHIGIRE");
        }
        else
        {
            tidaIsEnraged = false;
            StopCoroutine(TidaEnraged());
        }
    }

    private void OnEnable()
    {
        TriggerAreaScript.IsOpen += TidaEngagesCombat;
        TriggerAreaScript.IsClosed += TidaFinishedCombat;
        PlayerController.TEOnCooldown += TEIsOnCooldown; 
        //PlayerController.TSOnCooldown += TSIsOnCooldown;
    }
    private void OnDisable()
    {
        TriggerAreaScript.IsOpen += TidaEngagesCombat;
        TriggerAreaScript.IsClosed += TidaFinishedCombat;
        PlayerController.TEOnCooldown += TEIsOnCooldown;
        //PlayerController.TSOnCooldown += TSIsOnCooldown;
    }



    IEnumerator TelekineticErasureCooldown()
    {
        GetComponent<PlayerController>().TEIsGo = false;
        yield return new WaitForSeconds(DataCodex.instance.TelekineticErasureCooldown);
        GetComponent<PlayerController>().TEIsGo = true;
    }

    //IEnumerator TelekineticSlashCooldown()
    //{
    //    GetComponent<PlayerController>().TSIsGo = false;
    //    yield return new WaitForSeconds(DataCodex.instance.TelekineticSlashCooldown);
    //    GetComponent<PlayerController>().TSIsGo = true;
    //}

    IEnumerator TidaEnraged() 
    {
        OnTidaEnraged?.Invoke(this, EventArgs.Empty);

        foreach (ArmourModel armour in armourObjList)
        {
            armour.skin.GetComponent<SpriteRenderer>().material = armour.armourMaterialEnraged;
            //armour.skin.GetComponent<SpriteRenderer>().color = Color.red;
            //armour.armourMaterial.SetFloat("1EnergyScale", 5f);
            //armour.armourMaterial.SetColor("1EnergyColour", Color.red);
            //armour.skin.GetComponent<SpriteRenderer>().material.SetColor("1EnergyColour", Color.red);
            //armour.skin.GetComponent<SpriteRenderer>().material = armour.armourMaterialEnraged;
        }
        //enragedAnim.gameObject.SetActive(true);



        yield return new WaitForSeconds(tidaEnrageDuration);
        //enragedAnim.gameObject.SetActive(false);
        tidaFury = 0;

        yield return
        tidaIsEnraged = false;
        foreach (ArmourModel armour in armourObjList)
        {            
            armour.skin.GetComponent<SpriteRenderer>().material = armour.armourMaterial;
            //armour.skin.GetComponent<SpriteRenderer>().color = Color.white;
            //armour.armourMaterial.SetFloat("1EnergyScale", 5f);
            //armour.armourMaterial.SetColor("1EnergyColour", Color.red);
            //armour.skin.GetComponent<SpriteRenderer>().material.SetColor("1EnergyColour", Color.red);
            //armour.skin.GetComponent<SpriteRenderer>().material = armour.armourMaterialEnraged;
        }
        tidaMadnessLevel = tidaMadnessLevel + 50f;      
    }

    
    [System.Serializable]
    public class ArmourModel
    {
        public GameObject skin;
        public Material armourMaterial;
        public Material armourMaterialEnraged;
    }
}
