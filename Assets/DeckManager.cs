using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DeckManager : Singleton<DeckManager>
{
    public List<int> playerDeck;
    public List<int> opponentDeck;

    public  List<CardObject> playerCards;
    public  List<CardObject> opponentCards;
    public PhotonView photonView;
    
    public void GetCards()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < 30; i++)
            {
                int playerIndex = Random.Range(0, playerDeck.Count);
                int opponentIndex = Random.Range(0, opponentDeck.Count);
                
                photonView.RPC(nameof(GetMasterCard), RpcTarget.MasterClient, playerIndex);
                photonView.RPC(nameof(GetOpponentCard), RpcTarget.MasterClient, opponentIndex);
                photonView.RPC(nameof(GetMasterCard), RpcTarget.Others, opponentIndex);
                photonView.RPC(nameof(GetOpponentCard), RpcTarget.Others, playerIndex);
            }
        }
    }
    [PunRPC]
    public void GetMasterCard(int cardIndex)
    {
        CardObject playerCard = CardDatabase.Instance.FetchCardObjectById(cardIndex);
        playerCards.Add(playerCard);
    }
    [PunRPC]
    public void GetOpponentCard(int cardIndex)
    {
        CardObject opponentCard = CardDatabase.Instance.FetchCardObjectById(cardIndex);
        opponentCards.Add(opponentCard);
    }

    public void RemoveCard(CardSide side, CardObject cardObject)
    {
        if (side == CardSide.Player)
        {
            if (IsAvailible(side, cardObject))
            {
                playerCards.Remove(cardObject);
            }
        }
        else
        {
            if (IsAvailible(side, cardObject))
            {
                opponentCards.Remove(cardObject);
            }
        }
    }

    public bool IsAvailible(CardSide side, CardObject cardObject)
    {
        List<CardObject> cards = side == CardSide.Player ? playerCards : opponentCards;

        foreach (CardObject targetCard in cards)
        {
            if (cardObject.id == targetCard.id)
            {
                return true;
            }
        }
        return false;
    }
}