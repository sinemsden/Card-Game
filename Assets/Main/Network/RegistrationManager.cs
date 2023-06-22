using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputConfirmPassword;
    [SerializeField] private TextMeshProUGUI messageText;
    public void Register()
    {
        string username = inputUsername.text;
        string email = inputEmail.text;
        string password = inputPassword.text;
        string confirmPassword = inputConfirmPassword.text;

        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = inputUsername.text,
            Email = inputEmail.text,
            Password = inputPassword.text,
            RequireBothUsernameAndEmail = false
            
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult Result)
    {   
        messageText.text = "New account is created.";
        messageText.color = Color.green;

        MainMenuButtons.Instance.Invoke("ActivateStartScreen", 1f);
        MainMenuButtons.Instance.Invoke("ActivateLoginScreen", 1f);
    }

    private void OnError(PlayFabError Error)
    {
        messageText.text = "Somethings gone wrong.";
        messageText.color = Color.red;
        
        throw new NotImplementedException();
    }
}