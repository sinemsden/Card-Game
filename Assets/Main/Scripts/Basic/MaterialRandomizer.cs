using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizer : MonoBehaviour
{
    [SerializeField] Material[] material;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = material[Random.Range(0,material.Length)];
        Destroy(this);
    }
}
