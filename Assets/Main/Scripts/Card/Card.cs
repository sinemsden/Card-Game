using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon;
using Photon.Pun;
using Sirenix.OdinInspector;
using DG.Tweening;

public enum CardSide
{
    Player,
    Opponent,
}
public enum CardState
{
    Open,
    Closed,
    Used,
}
public enum EffectType
{
    Heal,
    Attack
}
[Serializable]
public struct Property
{
    [Header("Card Info")]
    public MeshRenderer image;
    public TextMeshPro titleText;
    public TextMeshPro descriptionText;
    public GameObject unusedFrame;
    public GameObject usedFrame;

    [Header("Card Type")]
    public TextMeshPro textMinion;
    public TextMeshPro textSpell;

    [Header("Card Stat Texts")]
    public TextMeshPro manaCostText;
    public TextMeshPro healthValueText;
    public TextMeshPro attackValueText;
    
    [Header("Card Effects")]
    public ParticleSystem cardEf0;
}

public abstract class Card : InstanceBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshPro IdNumber;
    public TextMeshPro IdNumber2;
    public Property property;
    public CardObject cardObject;
    public Slot slot;
    public CardSide side;
    public CardState state = CardState.Closed;
    [HideInInspector] public OutlineController outlineController;
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public Vector3 originalScale;
    private bool _isUsable = false;
    public bool IsUsable
    {
        get
        {
            return _isUsable;
        }
        set
        {
            if (value == _isUsable) { return; }

            if (value == true)
            {
                property.cardEf0.Play();
            }
            else
            {
                property.cardEf0.Stop();
            }
            _isUsable = value;
        }
    }

    private void Awake()
    {
        view = PhotonView.Get(this);
        InitComponents();
        InitCardProperties();
    }
    
    public virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        originalScale = transform.localScale;
    }
    public virtual void OnDisable()
    {
        transform.localScale = originalScale;
        
        SetState(CardState.Open);
        
        if (InputManager.selectedCard == this)
        {
            InputManager.selectedCard = null;
        }
    }
    
    private void InitComponents()
    {
        outlineController = GetComponent<OutlineController>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void SetCardObject(CardObject cardObject)
    {
        this.cardObject = cardObject;

        property.usedFrame.SetActive(false);
        property.unusedFrame.SetActive(true);
        
        InitCardProperties();
        ExecuteStartAnimation();
    }

    public virtual void InitCardProperties()
    {
        property.titleText.text = cardObject.title;
        property.descriptionText.text = cardObject.description;
        property.manaCostText.text = cardObject.manaCost.ToString();
        property.image.material.mainTexture = cardObject.image.texture;

        if (outlineController != null)
        {
            outlineController.SetOutline(false);
        }
    }

    public void ExecuteStartAnimation()
    {
        float startPosY = side == CardSide.Player ? -5 : 5;
        transform.localPosition = Vector3.zero;
        rectTransform.anchoredPosition = new Vector2(0, startPosY);
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutExpo);
    }

    [Button("SetState")]
    public void SetState(CardState cardState)
    {
        if (cardState != CardState.Used)
        {
            float targetRotation = cardState == CardState.Open ? 0 : 180;
            transform.DORotate(new Vector3(transform.localEulerAngles.x, targetRotation, transform.localEulerAngles.z), 0.5f);
        
            // if (state != CardState.Used)
            // {
            //     property.usedFrame.SetActive(false);
            //     property.unusedFrame.SetActive(true);
            // }
        }
        else
        {
            float targetRotation = 0;
            transform.DORotate(new Vector3(transform.localEulerAngles.x, targetRotation, transform.localEulerAngles.z), 0.5f);
        }    
                
        if (cardState != state)
        {
            transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 0, 1);
        }
                
        state = cardState;
    }

    [Button("SetSide")]
    public void SetSide(CardSide cardSide)
    {
        if (state == CardState.Used) { return; }
        
        float targetRotation = cardSide == CardSide.Player ? 0 : 180;
        transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRotation), 0.5f);

        side = cardSide;
//        Debug.Log(gameObject.name + " is " + side.ToString() + " now!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (side != CardSide.Player && state != CardState.Used) { return; }

        if (side != GameManager.turnOwner && InputManager.selectedCard == null)
        {
            return;
        }

        InputManager.OnCardMouseOver.Invoke(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if ((side != CardSide.Player && state != CardState.Used) || this == InputManager.selectedCard) { return; }
        
        if (side != GameManager.turnOwner && InputManager.selectedCard == null)
        {
            return;
        }

        if (InputManager.selectedCard != this)
        {
            InputManager.OnCardMouseExit.Invoke(this);
        }
    }
    public virtual void OnPointerClick(PointerEventData eventData)
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
                InputManager.OnCardSelected.Invoke(this);
            }
            else
            {
                //Has been attacked
                Debug.Log(side.ToString() + InputManager.selectedCard.name.ToString() + " side: " + InputManager.selectedCard.side);
            
                InputManager.selectedCard.view.RPC(nameof(EffectTo), RpcTarget.All, id);
            }
        }
        else
        {
            InputManager.selectedCard = null;
            if (state != CardState.Used)
            { 
                if (IsUsable == true)
                {
                    Debug.Log(state);
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

    public void MouseOver(bool isMouseOver)
    {
        outlineController.SetOutline(isMouseOver);
        
        if (state != CardState.Used) 
        {
            if (isMouseOver == true)
            {
                rectTransform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutExpo);
                rectTransform.DOAnchorPos(Vector2.zero + Vector2.up, 0.15f);
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, -1f);
            }
            else
            {
                rectTransform.DOScale(originalScale, 0.15f).SetEase(Ease.OutExpo);
                rectTransform.DOAnchorPos(Vector2.zero, 0.15f);
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
                property.cardEf0.Stop();
            }
        }
    }
    
    [PunRPC]
    public virtual void PutToTable(Slot slot)
    {
        if (side != GameManager.turnOwner && InputManager.selectedCard == null)
        {
            return;
        }
        property.cardEf0.Stop();
    }

    public bool CheckIsUsable()
    {
        if (Mathf.Abs(rectTransform.anchoredPosition.y) > 1.75f)
        {
            IsUsable = true;
            return true;
        }
        else
        {
            IsUsable = false;
            return false;
        }
    }

    public void Selected(bool isSelected)
    {    
        if (isSelected == true)
        {
            
        }
    }

    public void MoveToParent()
    {
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutExpo);
        outlineController.SetOutline(false);
        property.cardEf0.Stop();
    }

    public void ServerEffectTo(int effectedId)
    {
        if (this is MinionCard)
        {
            view.RPC(nameof(EffectTo), RpcTarget.All, effectedId);
        }
        
        InstanceBase effectedInstance;

        if (effectedId > 300)
        {
            bool isPlayer = PhotonNetwork.IsMasterClient == true ? true : false;
            GameManager.view.RPC("EffectToPlayer", RpcTarget.MasterClient, id, isPlayer);
        }
        else
        {
            effectedInstance = CardManager.FetchCardById(effectedId);
            MinionCard minionCard = effectedInstance.gameObject.GetComponent<MinionCard>();

            minionCard.view.RPC(nameof(WasEffected), RpcTarget.All, id, this.cardObject.effectValue);
        }
    }

    [PunRPC]
    public override void EffectTo(int effectedId)
    {

    }

    public virtual void Die()
    {
        CardManager.activeCards.Remove(this);
        if (InputManager.selectedCard == this)
        {
            InputManager.selectedCard = null;
        }
    }
}