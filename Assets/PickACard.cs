using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PickACard : Singleton<PickACard>
{
    public RectTransform rectTransform;
    public List<CollectionCard> collectionCards;
    public int cardSetIndex = 0;

    [Button("Appear")]
    public void Appear()
    {
        SetCards();
        rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
    }

    [Button("SetCards")]
    public void SetCards()
    {
        List<CardObject> allCards = CardDatabase.Instance.cardObjects;

        for(int i = 0; i < allCards.Count; i++)
        {
            int cardId = allCards[i].id;

            if (!MultiplayerCardManager.Instance.cardIds.Contains(cardId))
            {
                CardObject cardObject = CardDatabase.Instance.FetchCardObjectById(cardId);

                collectionCards[cardSetIndex].SetCardObject(cardObject);
                cardSetIndex++;
                
                if (cardSetIndex > 2)
                {
                    break;
                }
            }
        }
    }

    public void Pick(CollectionCard selectedCard)
    {
        CardDatabase.choosenCard = selectedCard.cardObject.id;

        MultiplayerCardManager.Instance.AddCard(selectedCard.cardObject.id);
        GameManager.Instance.Invoke("ReturnToMainMenu", 1.5f);
    }
}