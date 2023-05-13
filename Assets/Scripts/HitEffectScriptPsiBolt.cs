using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectScriptPsiBolt : MonoBehaviour
{

    void Update()
    {
        Destroy(gameObject, .75f);
    }
}
