using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : Singleton<DebugManager>
{
    public CardObject cardObjectReference;
    public Hand playerHand;
    public Hand opponentHand;

   

    public void DrawACard_ButtonClick(bool isPlayer)
    {
        DrawACard(isPlayer);
    }

    public void PlayACard_ButtonClick(bool isPlayer)
    {
        PlayACard(isPlayer);
    }

    public void DrawACard(bool isPlayer, bool triggeredFromNetwork = false)
    {
        HandManager.Instance.AddCardToHand(isPlayer == true ? CardSide.Player : CardSide.Opponent, cardObjectReference);        
    
        if (triggeredFromNetwork == false)
        {
            //Network_DrawACard(isPlayer);  
        }
    }

    public void PlayACard(bool isPlayer, bool triggeredFromNetwork = false)
    {
        if (isPlayer == true)
        {
            foreach(Slot slot in playerHand.cardSlots)
            {
                TableManager.Instance.AddCardToTable(slot.card);
                break;
            }
        }
        else
        {
            foreach(Slot slot in opponentHand.cardSlots)
            {
                TableManager.Instance.AddCardToTable(slot.card);
                break;
            }
        }
        if (triggeredFromNetwork == false)
        {
            //Network_PlayACard(isPlayer);
        }
    }

    
}