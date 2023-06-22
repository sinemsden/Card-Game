using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon;
using Photon.Pun;
public class SpellCard : Card
{
    public EffectType effectType;
    public int effectValue;
    public override void InitCardProperties()
    {
        base.InitCardProperties();

        property.textMinion.gameObject.SetActive(false);
        property.textSpell.gameObject.SetActive(true);
        property.healthValueText.transform.parent.gameObject.SetActive(false);
        property.attackValueText.transform.parent.gameObject.SetActive(false);
    }
    
    public override void PutToTable(Slot slot)
    {  
        Vector3 playerPosition = side == CardSide.Player ? GameManager.Instance.player.transform.position : GameManager.Instance.opponent.transform.position;
        Debug.Log("Spell card played, id: " + id);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOMove(new Vector3(playerPosition.x, playerPosition.y, transform.position.z), 0.5f).SetEase(Ease.InCubic));
        rectTransform.DOScale(originalScale * 0.8f, 0.5f).SetEase(Ease.InCubic).OnComplete(()=>
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

            InputManager.selectedCard = this;
        });
    }
    
    [PunRPC]
    public override void EffectTo(int effectedId)
    {
        InstanceBase effectedInstance;

        if (effectedId > 300)
        {
            if (side == CardSide.Player)
            {
                effectedInstance = GameManager.Instance.opponent;
            }
            else
            {
                effectedInstance = GameManager.Instance.player;
            }
        }
        else
        {
            effectedInstance = CardManager.FetchCardById(effectedId);
        
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, effectedInstance.transform.position.z - 1);
        Vector3 punchDirection = this.transform.position - effectedInstance.transform.position;
        
        SoundManager.Instance.PlaySound("Attack");

        Sequence attackSequence = DOTween.Sequence();

        if (effectType == EffectType.Attack)
        {
            attackSequence.Append(transform.DOPunchPosition(-punchDirection * 0.75f, 0.5f, 0, 1).OnComplete(() => 
            {    
                SimpleCameraShakeInCinemachine.Instance.Shake();
                base.ServerEffectTo(effectedId);
                Debug.Log("SpellCardPLAYED: " + base.id);
                Invoke(nameof(Die),0.1f);
            }));
        }
        else if (effectType == EffectType.Heal)
        {
            attackSequence.Append(transform.DOPunchPosition(-punchDirection * 0.5f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() => 
            {    
                base.ServerEffectTo(effectedId);
                Debug.Log("SpellCardPLAYED: " + base.id);
                Invoke(nameof(Die),0.1f);
            }));
        }
    }

    public override void Die()
    {
        EffectHolder.Instance.Play("Destroy", transform.position);
        GameManager.Instance.Server_UpdateCardCount(this, false);

        Debug.Log("SpellCardDestroyed: " + base.id);


        TableManager.Instance.view.RPC("RemoveCardFromTable", RpcTarget.All, id);
        base.Die();
        gameObject.SetActive(false);
    }
}