using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] Texture[] texture;

    Vector2 lastPos;

    private void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            lastPos = Input.mousePosition;
            //transform.localPosition = Camera.main.ScreenToViewportPoint(touch.position);
            GUI.DrawTexture(new Rect(lastPos.x - 84, -lastPos.y + Screen.height,256,256), texture[0], ScaleMode.ScaleToFit);
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = transform.rotation * Quaternion.Euler(0,0,-30);
    }
}
