using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class CardDatabase : Singleton<CardDatabase>
{
    public static CardDatabase classInstance;
    public List<CardObject> cardObjects;
    public static int choosenCard = -1;

    private void Start()
    {
        SceneManager.sceneLoaded += GetACard;

        DontDestroyOnLoad(this);
		
        if (classInstance == null) 
        {
            classInstance = this;
        }
         else 
        {
            Destroy(gameObject);
        }
    }

    public CardObject FetchCardObjectById(int id)
    {
        foreach(CardObject cardObject in cardObjects)
        {
            if (cardObject.id == id)
            {
                return cardObject;
            }
        }
        return null;
    }

    public void GetACard(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Init")
        {
            Debug.Log(scene.name + " loaded");
            MainMenuButtons.Instance.ActivateUserScreen();

            if (choosenCard < 0)
            {
                return;
            }

            DeckbuildingScreen.Instance.targetCard.gameObject.SetActive(true);

            CardObject cardObject = FetchCardObjectById(choosenCard);
            DeckbuildingScreen.Instance.targetCard.SetCardObject(cardObject);
        }
    }
}