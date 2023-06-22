using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public abstract class InstanceBase : MonoBehaviour
{
    public int id;
    public PhotonView view;


    public virtual void WasEffected(int effectorCardId, int effectValue)
    {
        
    }

    public virtual void EffectTo(int id)
    {
     
    }
}