using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
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