using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeOrder
{
    public Attribute name;
    public Transform transform;
    public List<int> cards;
}

public class DeckbuildingScreen : Singleton<DeckbuildingScreen>
{
    public CollectionCard cardPrefab;
    public List<AttributeOrder> atributeOrder;
    public CollectionCard targetCard;
    public void SetCards()
    {
        List<int> cardIdList = MultiplayerCardManager.Instance.cardIds;

        for(int i = 0; i < cardIdList.Count; i++)
        {
            CardObject cardObject = CardDatabase.Instance.FetchCardObjectById(cardIdList[i]);

            CollectionCard newCard = Instantiate(cardPrefab); 

            newCard.SetCardObject(cardObject);
            
            Transform attributeTransform;

            for (int j = 0; j < atributeOrder.Count; j++)
            {
                if (atributeOrder[j].name == cardObject.attribute)
                {
                    attributeTransform = atributeOrder[j].transform;

                    atributeOrder[j].cards.Add(cardIdList[i]);
                    
                    newCard.transform.SetParent(attributeTransform);
                }
            }

            newCard.transform.localPosition = Vector3.zero;
        }
    }
    public void AddCardToAttributeOrder(CollectionCard collectionCard)
    {
        Attribute cardAttribute = collectionCard.cardObject.attribute;

        // Find the target attribute order
        AttributeOrder targetAttributeOrder = atributeOrder.Find(order => order.name == cardAttribute);

        if (targetAttributeOrder != null)
        {
            // Add the card ID to the target attribute order's cards list
            targetAttributeOrder.cards.Add(collectionCard.cardObject.id);
            
            // Set the parent of the collectionCard's transform to the target attribute transform
            collectionCard.transform.SetParent(targetAttributeOrder.transform);
        }
    }
}