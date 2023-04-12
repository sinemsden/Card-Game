using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class LookAtConstraintManager : MonoBehaviour
{

    [SerializeField] LookAtConstraint lookAtConstraint;
    
    void Start()
    {
        lookAtConstraint = gameObject.GetComponent<LookAtConstraint>();

        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = Camera.main.transform;
        constraintSource.weight = 1;
        lookAtConstraint.AddSource(constraintSource);
    }
}
