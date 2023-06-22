using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Photon;
using Photon.Pun;
public class MinionCard : Card
{
    public int health = 0;
    private bool newCreated = true;
    public bool canAttack = true;

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.OnTurnStarted.AddListener(ResetCanAttack);
        canAttack = true;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnTurnStarted.RemoveListener(ResetCanAttack);
    }

    public void ResetCanAttack()
    {
        canAttack = true;
    }

    public override void InitCardProperties()
    {
        base.InitCardProperties();

        property.textMinion.gameObject.SetActive(true);
        property.textSpell.gameObject.SetActive(false);
        property.healthValueText.transform.parent.gameObject.SetActive(true);
        property.attackValueText.transform.parent.gameObject.SetActive(true);

        health = cardObject.health;
        
        newCreated = true;
        UpdateHealth(health);
        newCreated = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (side != CardSide.Player && state != CardState.Used) { return; }
        
        if (side != GameManager.turnOwner && InputManager.selectedCard == null)
        {
            return;
        }
        
       

        if (InputManager.selectedCard != this /*&& state != CardState.Used*/)
        {
            if (side != CardSide.Opponent)
            {
                if (InputManager.selectedCard is MinionCard || InputManager.selectedCard == null)
                {
                    if (canAttack == false)
                    {
                        return;
                    }
                    InputManager.OnCardSelected.Invoke(this);
                }
                else
                {
                    if (InputManager.selectedCard != null)
                    {   
                        Debug.Log("Input selected Card: " + InputManager.selectedCard.cardObject.title + "  |  This Card: " + this.cardObject.title);
                        InputManager.selectedCard.view.RPC(nameof(EffectTo), RpcTarget.All, id);
                        
                        if (cardObject.effectType == EffectType.Attack)
                        {
                            view.RPC(nameof(EffectTo), RpcTarget.All,  id);
                        }
                    }
                }
            }
            else
            {
                //Has been attacked
                Debug.Log(side.ToString() + InputManager.selectedCard.name.ToString() + " side: " + InputManager.selectedCard.side);
            
                InputManager.selectedCard.view.RPC(nameof(EffectTo), RpcTarget.All, id);
                if (cardObject.effectType == EffectType.Attack)
                {
                    view.RPC(nameof(EffectTo), RpcTarget.All,  id);
                }
            }
        }
        else
        {
            InputManager.selectedCard = null;
            if (state != CardState.Used)
            { 
                if (IsUsable == true)
                {
//                    Debug.Log(state);
                    if (cardObject.manaCost > GameManager.Instance.GetCurrentMana(side))
                    {
                        TMProScreenMessage.Instance.AppearMessage("Mana is not enough!", 0.5f, Color.red, 1);
                        MoveToParent();
                        return;
                    }
                    TableManager.Instance.view.RPC("AddCardToTable", Photon.Pun.RpcTarget.All, id);
                    Debug.Log("Card played, id: " + id);

                }
                else
                {
                    //release
                    MoveToParent();
                }
            }
        }
    }

    public void UpdateHealth(int newHealth)
    {
        if (newCreated == false)
        {
            if (health >= newHealth)
            {
                UIManager.Instance.SpawnNumber(NumberType.Damage, Mathf.Min(health - newHealth, health), transform.position);
            }
            else
            {
                UIManager.Instance.SpawnNumber(NumberType.Heal, Mathf.Min(newHealth - health, health), transform.position);
            }
        }

        health = newHealth;

        property.healthValueText.text = health.ToString();

        if (health <= 0)
        {
            Die();
        }
    }

    public override void PutToTable(Slot slot)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    {
        base.PutToTable(slot);
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOMove(slot.rectTransform.position, 0.5f).SetEase(Ease.InCubic));
        
        sequence.Append(rectTransform.DOScale(originalScale * 0.8f, 0.5f).SetEase(Ease.InCubic).OnComplete(()=>
        {
            property.usedFrame.SetActive(true);
            SpriteRenderer frameSprite = property.usedFrame.GetComponent<SpriteRenderer>();
            frameSprite.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            frameSprite.DOFade(1.0f, 0.2f);
            
            SoundManager.Instance.PlaySound("PlayCard");
            
            property.unusedFrame.SetActive(false);
            property.manaCostText.transform.parent.gameObject.SetActive(false);

            property.usedFrame.transform.localScale = Vector3.one * 0.3f;
            property.usedFrame.transform.DOScale(0.2f, 0.2f).SetEase(Ease.InCubic);
            
            transform.parent = slot.transform;
            slot.card = this;

            transform.position = new Vector3(transform.position.x, transform.position.y, slot.transform.position.z);

            HandManager.Instance.RemoveCardFromHand(side, this);

            GameManager.view.RPC("DecreaseMana", Photon.Pun.RpcTarget.All, cardObject.manaCost);

            this.slot = slot;
        }));
    }
    
    [PunRPC]
    public override void EffectTo(int effectedId)
    {
        InstanceBase effectedInstance;

        if (effectedId > 300)
        {
            effectedInstance = CardManager.FetchPlayerById(effectedId);
        }
        else
        {
            effectedInstance = CardManager.FetchCardById(effectedId);
        }

        
        if (InputManager.selectedCard == this)
        {
            InputManager.selectedCard = null;
        }
        outlineController.SetOutline(false);
        
        if (canAttack == false)
        {
            return;
        }
        canAttack = false;

        transform.position = new Vector3(transform.position.x, transform.position.y, effectedInstance.transform.position.z - 1);
        Vector3 punchDirection = this.transform.position - effectedInstance.transform.position;
        
        SoundManager.Instance.PlaySound("Attack");

        Sequence attackSequence = DOTween.Sequence();

        attackSequence.Append(transform.DOPunchPosition(-punchDirection * 0.75f, 0.5f, 0, 1).OnComplete(() => 
        {    
            SimpleCameraShakeInCinemachine.Instance.Shake();
            base.ServerEffectTo(effectedId);
        }));
    }

    [PunRPC]
    public override void WasEffected(int effectorCardId, int effectValue)
    {
        Card effectorCard;

        effectorCard = CardManager.FetchCardById(effectorCardId);

        Debug.Log("Was effected, effectorCardId: " + effectorCardId + "effectorCard: " + effectorCard.cardObject.name);

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

    public override void Die()
    {
        base.Die();

        EffectHolder.Instance.Play("Destroy", transform.position);
        GameManager.Instance.Server_UpdateCardCount(this, false);
        
        TableManager.Instance.RemoveCardFromTable(id);
        gameObject.SetActive(false);
    }
}