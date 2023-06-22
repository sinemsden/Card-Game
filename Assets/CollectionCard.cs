using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.Events;
public class CollectionCard : MonoBehaviour, IPointerDownHandler
{
    public Property property;
    public CardObject cardObject;
    public UnityEvent OnSelected;

    private void Awake()
    {
        SetCardObject(cardObject);
    }
    public void SetCardObject(CardObject cardObject)
    {
        this.cardObject = cardObject;

        property.usedFrame.SetActive(false);
        property.unusedFrame.SetActive(true);
        
        InitCardProperties();
    }

    public virtual void InitCardProperties()
    {
        property.titleText.text = cardObject.title;
        property.descriptionText.text = cardObject.description;
        property.manaCostText.text = cardObject.manaCost.ToString();
        property.image.material.mainTexture = cardObject.image.texture;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        OnSelected.Invoke();
    }
    public void SetParent()
    {
        
    }
}