using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private MultiplayerManager multiplayerManager;

    private void Start()
    {
        multiplayerManager = GetComponent<MultiplayerManager>();        
    }

    public void FindMatch() => multiplayerManager.FindMatch();
    public void Exit() => Application.Quit();
}