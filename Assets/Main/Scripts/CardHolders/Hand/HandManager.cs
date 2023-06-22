using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class HandManager : Singleton<HandManager>
{
    public Hand playerHand;
    public Hand opponentHand;

    [Button("AddCard")]
    public void AddCardToHand(CardSide side, CardObject cardObject)
    {
        Hand targetHand;

        if (side == CardSide.Player)
        {
            targetHand = playerHand;
        }
        else
        {
            targetHand = opponentHand;
        }

        targetHand.AddCard(cardObject);
    }
    public void RemoveCardFromHand(CardSide side, Card card)
    {
        Hand targetHand;
        
        if (side == CardSide.Player)
        {
            targetHand = playerHand;
        }
        else
        {
            targetHand = opponentHand;
        }

        targetHand.RemoveCard(card);
    }
}