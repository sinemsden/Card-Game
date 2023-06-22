using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        DontDestroyOnLoad(this);
		
        // if (classInstance == null) {
        //     classInstance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }

        // if (PhotonNetwork.IsMasterClient)
        // {
        //     PhotonNetwork.AutomaticallySyncScene = true;
        //     PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }

    public void FindMatch()
    {
        PhotonNetwork.ConnectUsingSettings();
        
        var requestUserName = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(requestUserName, OnGetAccountInfoUsernameSuccess, OnGetFailure);
    }

    private void OnGetAccountInfoUsernameSuccess(GetAccountInfoResult result)
    {

        string username = result.AccountInfo.TitleInfo.DisplayName;
        if (username != "")
        {
            PhotonNetwork.NickName = username;
            PhotonNetwork.LocalPlayer.NickName = username;   
        }else{
            PhotonNetwork.NickName = "Guest";
            PhotonNetwork.LocalPlayer.NickName = "Username";
        }
    }

    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        GameManager.Instance.UpdatePlayerNames();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameManager.Instance.UpdatePlayerNames();
    }

    private void OnGetFailure(PlayFabError error)
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon sunucusuna bağlandı");

        if (PhotonNetwork.AuthValues != null)
        {
            ExitGames.Client.Photon.Hashtable userProperties = new ExitGames.Client.Photon.Hashtable() {{"username", PhotonNetwork.AuthValues.UserId}};
            PhotonNetwork.LocalPlayer.SetCustomProperties(userProperties);
            //PhotonNetwork.PlayerList[0].SetCustomProperties(userProperties);
        }

        Debug.Log("Başarıyla giriş yaptınız!");

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
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
