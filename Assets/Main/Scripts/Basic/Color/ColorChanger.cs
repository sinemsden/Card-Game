using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color newColor;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public void ChangeTheMaterialColor()
    {
        skinnedMeshRenderer.material.color = newColor;
    }
}
