using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardHolderBase
{
    public override void AddCard(CardObject cardObject)
    {
        Slot newSlot = Instantiate(emptySlotPrefab, this.transform);
        cardSlots.Add(newSlot);

        UpdateSlotDepth();
        

        Card newCard; 
        if (cardObject is MinionCardObject)
        {
            newCard = ObjectPooler.Instance.GetMinionCard();
        }else{
            newCard = ObjectPooler.Instance.GetSpellCard();
        }

        newCard.gameObject.SetActive(true);

        newCard.slot = newSlot;

        newCard.SetSide(side);
        newCard.SetState(side == CardSide.Player ? CardState.Open : CardState.Closed);

        GameManager.Instance.Server_UpdateCardCount(newCard, true);

        newCard.SetCardObject(cardObject);
        
        newCard.transform.SetParent(newSlot.transform);
        newCard.transform.localPosition = Vector3.zero;
        newSlot.card = newCard;
    
    }
    public override void RemoveCard(Card card)
    {
        cardSlots.Remove(card.slot);
        Destroy(card.slot.gameObject);
        card.slot = null;
        UpdateSlotDepth();
    }

    public void UpdateSlotDepth()
    {
        for(int i = 0; i < cardSlots.Count; i++)
        {
//            Debug.Log(i);
            if(cardSlots[i] != null)
            {
                cardSlots[i].transform.localPosition = Vector3.back * i * 0.1f;
            }
        }
    }
}