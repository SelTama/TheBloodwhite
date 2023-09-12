using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenEffect : MonoBehaviour
{
    public Renderer2DData screenEffectsRenderer;
    public Material screenMat;
    public bool tidaEnraged;

    public float rippleEffectSpeed;
    public float rippleEffectSize;
    public float rippleEffectMagnitude;



    IEnumerator ScreenRipple()
    {

        Camera camObj = Camera.main;

        screenMat.SetFloat("_TimePassed", -float.MaxValue);
        screenMat.SetVector("_FocalPoint", camObj.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y)));
        screenMat.SetFloat("_EffectSpeed", rippleEffectSpeed);
        screenMat.SetFloat("_EffectSize", rippleEffectSize);
        screenMat.SetFloat("_EffectMagnitude", rippleEffectMagnitude);

        yield return new WaitForSeconds(1f);

        tidaEnraged = false;
        screenMat.SetFloat("_EffectSpeed", 0);
        screenMat.SetFloat("_EffectSize", 0);
        screenMat.SetFloat("_EffectMagnitude", 0);



    }




}
