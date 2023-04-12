using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;

public enum NumberType
{
    Heal,
    Damage,
    Mana
}
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private DamageNumber healNumber;
    [SerializeField] private DamageNumber damageNumber;
    [SerializeField] private DamageNumber manaNumber;

    [SerializeField] private TextMeshProUGUI playerManaText;
    [SerializeField] private TextMeshProUGUI opponentManaText;

    public void SpawnNumber(NumberType numberType, int number, Vector3 position)  
    {
        switch (numberType)
        {
            case NumberType.Damage:
                damageNumber.Spawn(position, number);
            break;
            case NumberType.Heal:
                healNumber.Spawn(position, number);
            break;
        }
    }

    public void UpdateMana(int current, int max, int writeValue, CardSide side)
    {
        if (side == CardSide.Player)
        {
            playerManaText.text = current.ToString() + "/" +  max.ToString();
            manaNumber.Spawn(playerManaText.transform.position, writeValue);
        }
        else
        {
            opponentManaText.text = current.ToString() + "/" +  max.ToString();
            
            DamageNumber newNumber = manaNumber.Spawn(opponentManaText.transform.position, writeValue);
        }
    }
}