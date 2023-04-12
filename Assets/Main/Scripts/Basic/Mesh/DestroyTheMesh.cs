using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheMesh : MonoBehaviour
{
    private void Start()
    {
        Destroy(GetComponent<MeshFilter>());
        Destroy(GetComponent<MeshRenderer>());
    }
}
