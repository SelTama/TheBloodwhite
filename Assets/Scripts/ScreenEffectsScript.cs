using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenEffectsScript : MonoBehaviour
{
    public Renderer2DData screenEffectsRenderer;
    public Material screenMat;

    public float rippleEffectSpeed;
    public float rippleEffectSize;
    public float rippleEffectMagnitude;

    private void Start()
    {
        TidaStateScript tidaStateScript = GetComponent<TidaStateScript>();
        tidaStateScript.OnTidaEnraged += TidaStateScript_OnTidaEnraged;
    }

    private void TidaStateScript_OnTidaEnraged(object sender, EventArgs e)
    {
        StartCoroutine(ScreenRipple());
    }

    IEnumerator ScreenRipple()
    {

        Camera camObj = Camera.main;

        screenMat.SetFloat("_TimePassed", 0f);
        screenMat.SetVector("_FocalPoint", camObj.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y)));
        screenMat.SetFloat("_EffectSpeed", rippleEffectSpeed);
        screenMat.SetFloat("_EffectSize", rippleEffectSize);
        screenMat.SetFloat("_EffectMagnitude", rippleEffectMagnitude);

        yield return new WaitForSeconds(1f);

        rippleEffectMagnitude = Mathf.SmoothDamp(rippleEffectMagnitude, 0, ref rippleEffectSpeed, .1f);
        screenMat.SetFloat("_EffectSpeed", 0);
        screenMat.SetFloat("_EffectSize", 0);
        screenMat.SetFloat("_EffectMagnitude", 0);


    }






}
