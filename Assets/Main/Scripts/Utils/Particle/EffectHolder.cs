using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolder : Singleton<EffectHolder>
{
    public ParticleSystem destroyEffect;

    public void Play(string effect, Vector3 position)
    {
        if (effect == "Destroy")
        {
            Instantiate(destroyEffect, position, destroyEffect.transform.rotation);
        }
    }
}