using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardManager : MonoBehaviour
{
    public static List<Card> activeCards = new List<Card>();
    public static List<PlayerInstance> playerInstances = new List<PlayerInstance>();
    public static Card FetchCardById(int id)
    {
        foreach(Card card in activeCards)
        {
            if (card.id == id)
            {
                return card;
            }
        }
        return null;
    }
    public static PlayerInstance FetchPlayerById(int id)
    {
        foreach(PlayerInstance player in playerInstances)
        {
            if (player.id == id)
            {
                return player;
            }
        }
        return null;
    }

    private void OnEnable()
    {
        InputManager.OnCardMouseOver.AddListener(HandleCardMouseOver);
        InputManager.OnCardMouseExit.AddListener(HandleCardMouseExit);
        InputManager.OnCardSelected.AddListener(HandleCardSelected);
    }

    public void HandleCardMouseOver(Card card)
    {
        card.MouseOver(true);
    }
    public void HandleCardMouseExit(Card card)
    {
        card.MouseOver(false);
    }
    public void HandleCardSelected(Card card)
    {
        card.Selected(true);
    }
}