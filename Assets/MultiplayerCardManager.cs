using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class MultiplayerCardManager : Singleton<MultiplayerCardManager>
{
    public static MultiplayerCardManager classInstance;
    public List<int> cardIds = new List<int>();
    private void Start()
    {
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
 
    public void SavePlayerCardIDs(List<int> cardIDs)
    {
        // Kart ID'lerini PlayerPrefs ile kaydet
        string cardIDsString = string.Join(",", cardIDs);
        PlayerPrefs.SetString("CardIDs", cardIDsString);
        PlayerPrefs.Save();
    }

    public void AddCard(int newCardID)
    {
        cardIds.Add(newCardID);
        SavePlayerCardIDs(cardIds);
    }

    public void LoadPlayersCardDeck()
    {
        cardIds = LoadPlayerCardIDs();
    }

    public List<int> LoadPlayerCardIDs()
    {
        // Kaydedilmiş kart ID'lerini PlayerPrefs'ten yükle
        if (PlayerPrefs.HasKey("CardIDs"))
        {
            string cardIDsString = PlayerPrefs.GetString("CardIDs");
            string[] cardIDsArray = cardIDsString.Split(',');
            List<int> cardIDs = new List<int>();

            foreach (string cardIDString in cardIDsArray)
            {
                int cardID;
                if (int.TryParse(cardIDString, out cardID))
                {
                    cardIDs.Add(cardID);
                }
            }

            return cardIDs;
        }
        else
        {
            // Kaydedilmiş kart ID'leri bulunamadığında boş bir liste döndür
            Debug.Log("yeni liste kaydedildi.");
            List<int> newCardIDs = new List<int>() {0, 1, 2, 3, 4, 5, 6};
            SavePlayerCardIDs(newCardIDs);
            return newCardIDs;
        }
    }

}