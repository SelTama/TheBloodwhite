using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenEffectApplier : MonoBehaviour
{

    public delegate void ScreenEffectAppliedEventHandler(object source, EventArgs args);
    public event ScreenEffectAppliedEventHandler ScreenEffectApplied;


    public Renderer2DData screenEffectsRenderer;
    public Material screenMat;
    public bool tidaEnraged;

    public float rippleEffectSpeed;
    public float rippleEffectSize;
    public float rippleEffectMagnitude;


    protected virtual void OnScreenEffectApplied(Coroutine coroutine)
    {
        if (ScreenEffectApplied != null)
            ScreenEffectApplied(this, EventArgs.Empty);
    }

    public void ApplyScreenEffect(Coroutine coroutine) 
    {

    }


  


}
