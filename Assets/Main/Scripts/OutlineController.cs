using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
public class OutlineController : MonoBehaviour
{
    Outline[] outlines;

    private void Awake()
    {
        outlines = GetComponentsInChildren<Outline>();
        Invoke(nameof(InitOutline),0.1f);
    }

    void InitOutline()
    {
        SetOutline(false);
    }


    public void SetOutline(bool isActive)
    {
        if (outlines == null) { return; }

        foreach (Outline outline in outlines)
        {            
            outline.enabled = isActive;
        }
    }
}
