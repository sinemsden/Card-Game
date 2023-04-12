using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    [SerializeField] Animator animator;
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ActivateRagdoll(false);
    }
    public void ActivateRagdoll(bool isActive)
    {
        animator.enabled = !isActive;
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !isActive;
        }
    }
}