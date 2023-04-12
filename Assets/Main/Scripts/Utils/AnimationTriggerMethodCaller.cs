using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTriggerMethodCaller : MonoBehaviour
{
    public UnityEvent OnAnimationTrigger;
    
    public void AnimationTrigger()
    {
        OnAnimationTrigger.Invoke();
    }
}