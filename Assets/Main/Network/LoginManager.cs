using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TextMeshProUGUI messageText;

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = inputEmail.text,
            Password = inputPassword.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void LoginAsAkduman()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = "akdumanmehmet2002@gmail.com",
            Password = "Akduman147"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void LoginAsVuthax()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = "vuthax2002@gmail.com",
            Password = "Akduman147"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult Result)
    {   
        messageText.text = "Login in.";
        messageText.color = Color.green;

        MainMenuButtons.Instance.ActivateStartScreen();
        MainMenuButtons.Instance.ActivateUserScreen();
    }

    private void OnError(PlayFabError Error)
    {
        messageText.text = "Somethings gone wrong.";
        messageText.color = Color.red;
        
        throw new NotImplementedException();
    }
}