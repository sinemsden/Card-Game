using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using DG.Tweening;

public class MainMenuButtons : Singleton<MainMenuButtons>
{
    [SerializeField] private MultiplayerManager multiplayerManager;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject registerScreen;
    [SerializeField] private GameObject userScreen;
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private GameObject deckbuildingMenu;
    [SerializeField] private Image startBg;
    [SerializeField] private RectTransform startText;


    private void Start()
    {
        //multiplayerManager = GetComponent<MultiplayerManager>();        
    }

    public void SetDeckbuildingActivity(bool isActive)
    {
        deckbuildingMenu.SetActive(isActive);
        if (isActive == true)
        {
            deckbuildingMenu.transform.localPosition = Vector3.zero - new Vector3(0, 0, 443);
        }
    }

    public void ActivateStartScreen()
    {
        startScreen.SetActive(true);
        loginScreen.SetActive(false);
        registerScreen.SetActive(false);
        userScreen.SetActive(false);
    }
    public void ActivateLoginScreen()
    {
        startScreen.SetActive(false);
        loginScreen.SetActive(true);
    }
    public void ActivateRegisterScreen()
    {
        startScreen.SetActive(false);
        registerScreen.SetActive(true);
    }
    public void ActivateUserScreen(string username = "")
    {
        startScreen.SetActive(false);
        loginScreen.SetActive(false);
        registerScreen.SetActive(false);
        userScreen.SetActive(true);

        var requestUserName = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(requestUserName, OnGetAccountInfoUsernameSuccess, OnGetFailure);
        MultiplayerCardManager.Instance.LoadPlayersCardDeck();
        DeckbuildingScreen.Instance.SetCards();
        SetDeckbuildingActivity(false);

        if (PlayerPrefs.GetInt("firstTime") == 0)
        {
            startBg.DOFade(0.8f, 1).OnComplete(()=>{
                startText.DOAnchorPosY(2264, 30).SetEase(Ease.Linear).OnComplete(()=>
                {
                    startBg.DOFade(0f, 1);
                });
            });

            PlayerPrefs.SetInt("firstTime", 1);
        }
    }

    private void OnGetAccountInfoUsernameSuccess(GetAccountInfoResult result)
    {

        string username = result.AccountInfo.TitleInfo.DisplayName;
        if (username != "")
        {
            welcomeText.text = "Welcome, " + username + "!";
            return;
        }
        welcomeText.text = "";
        
    }

    private void OnGetFailure(PlayFabError error)
    {
        
    }

    public void FindMatch() => multiplayerManager.FindMatch();
    public void Exit() => Application.Quit();
}