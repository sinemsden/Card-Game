using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TableManager : Singleton<TableManager>
{
    public Table playerTable;
    public Table opponentTable;
    [Button("AddCard")]
    public void AddCardToTable(Card card)
    {
        Table targetTable = GetTargetTable(card);

        targetTable.AddCard(card);
    }
    public void RemoveCardFromTable(Card card)
    {
        
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