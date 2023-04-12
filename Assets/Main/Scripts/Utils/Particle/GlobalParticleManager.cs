using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParticleManager : Singleton<GlobalParticleManager>
{
    public ParticleSystem waterInteractionEffect;
    public ParticleSystem waterSplatEffect;

    public Transform targetPosition;

    public void PlayParticleEffect(string name)
    {
        switch (name)
        {
            case "WaterInteraction":
                waterInteractionEffect.transform.position = targetPosition.position;
                waterInteractionEffect.Play();
                break;
            case "WaterSplat":
                waterSplatEffect.transform.position = targetPosition.position;
                waterSplatEffect.Play();
                break;
        }
    }
}