using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardHolderBase : MonoBehaviour
{
    public CardSide side;
    public Slot emptySlotPrefab;
    public List<Slot> cardSlots;

    public virtual void AddCard() { }
    public virtual void AddCard(CardObject cardObject) { }
    public virtual void AddCard(Card card) { }
    public virtual void RemoveCard() { }
    public virtual void RemoveCard(CardObject cardObject) { }
    public virtual void RemoveCard(Card card) { }
}
