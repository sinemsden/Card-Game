using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _startRotation;


    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _endRotation;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation.eulerAngles;
    }

    public void AnimateObject(float time)
    {
        //Use mathf sin
        float sinT = GetT(time);
        if (_endPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, sinT);
        }
        if (_endRotation != Vector3.zero)
        {
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(_startRotation, _endRotation, sinT));
        }
    }

    float GetT(float time)
    {
        return time;
    }
}
