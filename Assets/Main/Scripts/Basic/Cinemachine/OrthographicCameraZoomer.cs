using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OrthographicCameraZoomer : MonoBehaviour
{
    public float zoomSpeed = 0.5f;
    public float zoomIn = 5.0f;
    public float zoomOut = 20.0f;
    
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Zoom(bool isZoom)
    {
        if (isZoom)
        {
            StartCoroutine(ZoomIn());
        }
        else
        {
            StartCoroutine(ZoomOut());
        }
    }
    IEnumerator ZoomIn()
    {
        while (virtualCamera.m_Lens.OrthographicSize > zoomIn)
        {
            virtualCamera.m_Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ZoomOut()
    {
        while (virtualCamera.m_Lens.OrthographicSize < zoomOut)
        {
            virtualCamera.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
