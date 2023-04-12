using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstanceBase : MonoBehaviour
{
    public virtual void WasEffected(Card effectorCard, int effectValue)
    {
        
    }
    public virtual void EffectTo(InstanceBase effectedInstance)
    {
     
    }
}
