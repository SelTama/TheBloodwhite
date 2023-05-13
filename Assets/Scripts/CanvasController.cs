using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject furyBase;
    public GameObject furyFill;
    public GameObject madnessBase;
    public GameObject madnessFill;

    public TMPro.TextMeshProUGUI TECooldownDisplay;


    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        furyFill.GetComponent<CanvasRenderer>().GetComponent<Image>().fillAmount = playerObj.GetComponent<TidaStateScript>().tidaFury / playerObj.GetComponent<TidaStateScript>().tidaFuryLimit;
        //furyFill.transform.localScale = new Vector3(playerObj.GetComponent<TidaStateScript>().tidaFury / playerObj.GetComponent<TidaStateScript>().tidaFuryLimit, 1, 1);
        madnessFill.GetComponent<CanvasRenderer>().GetComponent<Image>().fillAmount = playerObj.GetComponent<TidaStateScript>().tidaMadnessLevel / playerObj.GetComponent<TidaStateScript>().tidaMadnessThreshhold;
        //madnessFill.transform.localScale = new Vector3(playerObj.GetComponent<TidaStateScript>().tidaMadnessLevel / playerObj.GetComponent<TidaStateScript>().tidaMadnessThreshhold, 1, 1);
    }





}
