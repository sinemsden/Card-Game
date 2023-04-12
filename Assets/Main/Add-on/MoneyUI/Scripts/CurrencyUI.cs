using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public Animator currencyTextAnimator;
    public TextMeshProUGUI currencyText;
    void Start()
    {
        currencyTextAnimator = gameObject.GetComponent<Animator>();
    }
    public void SetAppear(bool isShowing)
    {
        currencyTextAnimator.SetBool("isShowing",isShowing);
    }
    public void IncreaseAnimation()
    {
        currencyTextAnimator.SetTrigger("isIncreased");        
    }
}