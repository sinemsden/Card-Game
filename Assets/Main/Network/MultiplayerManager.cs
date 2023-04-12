using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Screen.fullScreen = true;

        DontDestroyOnLoad(gameObject);
    }

    public void FindMatch()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "zattirizortzort";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.CleanupCacheOnLeave = false;
        roomOptions.PublishUserId = true;


        PhotonNetwork.JoinRandomOrCreateRoom(null, roomOptions.MaxPlayers, MatchmakingMode.FillRoom, null, null, null, roomOptions, null);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
    }

    public override void OnCreatedRoom() { }

    public override void OnJoinedRoom()
    {
        // joined a room successfully, CreateRoom leads here on success

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
