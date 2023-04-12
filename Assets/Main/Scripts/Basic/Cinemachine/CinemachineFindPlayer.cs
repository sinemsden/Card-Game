using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineFindPlayer : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(FindPlayer), 0.1f);
    }
    void FindPlayer()
    {
        if (GetComponent<CinemachineStateDrivenCamera>() != null)
        {
            CinemachineStateDrivenCamera stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            stateDrivenCamera.m_LookAt = player.transform;
            stateDrivenCamera.m_Follow = player.transform;
            stateDrivenCamera.m_AnimatedTarget = player.gameObject.GetComponent<Animator>();
        }
        if (GetComponent<CinemachineVirtualCamera>() != null)
        {
            CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            virtualCamera.m_LookAt = player.transform;
            virtualCamera.m_Follow = player.transform;
        }
    }
}
