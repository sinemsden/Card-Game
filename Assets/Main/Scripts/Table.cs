using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : CardHolderBase
{
    public override void AddCard(Card card)
    {
        Slot newSlot;
        if (card is MinionCard)
        {
            newSlot = Instantiate(emptySlotPrefab, this.transform);
            cardSlots.Add(newSlot);

            newSlot.transform.localPosition = Vector3.zero;
            
            
        }
        else
        {
            if (card.side == CardSide.Player)
            {
                newSlot = GameManager.Instance.player.GetComponent<Slot>();
            }
            else
            {
                newSlot = GameManager.Instance.opponent.GetComponent<Slot>();
            }
        }
            StartCoroutine(PutToTable(card, newSlot));

        //card.SetState(CardState.Used);
        //card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, 0);

        card.SetState(CardState.Used);
        
    }
    public override void RemoveCard(Card card)
    {
        cardSlots.Remove(card.slot);
        card.transform.parent = null;
        Destroy(card.slot.gameObject);
        card.slot = null;
    }

    private IEnumerator PutToTable(Card card, Slot newSlot)
    {
        yield return new WaitForSeconds(0.1f);
        card.PutToTable(newSlot);
    }
}
 