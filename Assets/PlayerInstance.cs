using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public struct PlayerProperty
{
    public TextMeshPro healthValueText;
}

public class PlayerInstance : InstanceBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlayerProperty property;
    public int health = 30;
    public int maxMana = 1;
    public int currentMana = 1;
    public CardSide side;
    private OutlineController _outlineController;

    private void Start()
    {
        _outlineController = GetComponent<OutlineController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outlineController.SetOutline(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _outlineController.SetOutline(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InputManager.selectedCard != null)
        {
            if (InputManager.selectedCard.side == CardSide.Player && InputManager.selectedCard.state == CardState.Used)
            {
                InputManager.selectedCard.EffectTo(this);
            }
        }
    }
    
    public override void WasEffected(Card effectorCard, int effectValue)
    {
        switch (effectorCard.cardObject.effectType)
        {
            case EffectType.Attack:
                if(effectorCard is MinionCard)
                {
                    MinionCard effectorMinionCard = effectorCard.gameObject.GetComponent<MinionCard>();
                    int newHealth = Effects.GetAttackResult(health, effectorMinionCard.cardObject.effectValue);
                    UpdateHealth(newHealth);
                }
                else
                {
                    SpellCard effectorSpellCard = effectorCard.gameObject.GetComponent<SpellCard>();
                    int newHealth = Effects.GetAttackResult(health, effectorSpellCard.cardObject.effectValue);
                    UpdateHealth(newHealth);
                }
                break;
            case EffectType.Heal:
                if(effectorCard is MinionCard)
                {
                    MinionCard effectorMinionCard = effectorCard.gameObject.GetComponent<MinionCard>();
                    int newHealth = Effects.GetHealResult(health, effectorMinionCard.cardObject.effectValue);
                    UpdateHealth(newHealth);
                }
                else
                {
                    SpellCard effectorSpellCard = effectorCard.gameObject.GetComponent<SpellCard>();
                    int newHealth = Effects.GetHealResult(health, effectorSpellCard.cardObject.effectValue);
                    UpdateHealth(newHealth);
                }
                break;
            default:
                break;
        }
    }

    public void DecreaseMana(int value)
    {
        currentMana -= value;
        UIManager.Instance.UpdateMana(currentMana, maxMana, -value, side);
    }

    public void MaxMana()
    {
        maxMana++;
        currentMana = maxMana;
        UIManager.Instance.UpdateMana(currentMana, maxMana, 1, side);
    }
    
    public void UpdateHealth(int newHealth)
    {
        if (health >= newHealth)
        {
            UIManager.Instance.SpawnNumber(NumberType.Damage, Mathf.Min(health - newHealth, health), transform.position);
        }
        else
        {
            UIManager.Instance.SpawnNumber(NumberType.Heal, Mathf.Min(newHealth - health, health), transform.position);
        }

        health = newHealth;

        property.healthValueText.text = health.ToString();

        if (health <= 0)
        {
            GameManager.Instance.FinishTheMatch(side);
        }
    }
}
