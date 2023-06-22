using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon;
using Photon.Pun;
public class TableManager : Singleton<TableManager>
{
    public Table playerTable;
    public Table opponentTable;
    public PhotonView view;

    private void Start()
    {
        view = PhotonView.Get(this);
    }

    [PunRPC]
    public void AddCardToTable(int id)
    {
        Card card = CardManager.FetchCardById(id);

        Table targetTable = GetTargetTable(card);

        targetTable.AddCard(card);
    }

    [PunRPC]
    public void RemoveCardFromTable(int id)
    {
        Card card = CardManager.FetchCardById(id);
        
        Table targetTable = GetTargetTable(card);

        targetTable.RemoveCard(card);
    }

    public Table GetTargetTable(Card card)
    {
        Table targetTable;

        if (card.side == CardSide.Player)
        {
            targetTable = playerTable;
        }
        else
        {
            targetTable = opponentTable;
        }

        return targetTable;
    }
}