using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreatorAtStart : MonoBehaviour
{
    [SerializeField] GameObject shockwavePrefab;

    private void Start()
    {
        Instantiate(shockwavePrefab, transform.position, transform.rotation);
    }
}
