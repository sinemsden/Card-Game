using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DeckManager : Singleton<DeckManager>
{
    public List<int> playerDeck;
    public List<int> opponentDeck;

    public  List<CardObject> playerCards;
    public  List<CardObject> opponentCards;


    public void GetCards()
    {
        for (int i = 0; i < 30; i++)
        {
            int playerIndex = Random.Range(0, playerDeck.Count);
            int opponentIndex = Random.Range(0, opponentDeck.Count);
            
            CardObject playerCard = CardDatabase.Instance.FetchCardObjectById(playerIndex);
            CardObject opponentCard = CardDatabase.Instance.FetchCardObjectById(opponentIndex);
            
            playerCards.Add(playerCard);
            opponentCards.Add(opponentCard);
        }
    }

    public void RemoveCard(CardSide side, CardObject cardObject)
    {
        if (side == CardSide.Player)
        {
            if (IsAvailible(side, cardObject))
            {
                playerCards.Remove(cardObject);
            }
        }
        else
        {
            if (IsAvailible(side, cardObject))
            {
                opponentCards.Remove(cardObject);
            }
        }
    }

    public bool IsAvailible(CardSide side, CardObject cardObject)
    {
        List<CardObject> cards = side == CardSide.Player ? playerCards : opponentCards;

        foreach (CardObject targetCard in cards)
        {
            if (cardObject.id == targetCard.id)
            {
                return true;
            }
        }
        return false;
    }
}