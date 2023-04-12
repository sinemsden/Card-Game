using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class CardDatabase : Singleton<CardDatabase>
{
    public List<CardObject> cardObjects;

    [Button("GetCard")]
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
}