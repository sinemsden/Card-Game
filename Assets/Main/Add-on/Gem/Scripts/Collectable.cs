using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collectable : MonoBehaviour
{
    CurrencyUI currencyUi;
    public TextMeshPro text;
    public ParticleSystem collectedEffect;
    public Animation _animation;
    public int value;
    public int maxValue = 15;
    public bool isCollected = false;

    void Start()
    {
        if (!gameObject.CompareTag("Currency"))
        {
            SetValue();
        }
        else
        {
            CurrencyUI currencyUi = GameObject.FindGameObjectWithTag("CurrencyUI").GetComponent<CurrencyUI>();
        }
    }
    void SetValue()
    {
        value = Random.Range(1, maxValue);
        text.text = value.ToString();
    }
    public void Destroy()
    {
        _animation.Play("Collectable_Destroy");
    }
    public void DestroyIt()
    {
        _animation.Play("Gem_Destroy");
        collectedEffect.Play();
        currencyUi.IncreaseAnimation();
        currencyUi.SetAppear(true);
        Invoke(nameof(SetDeactive),0.1f);
    }

    void SetDeactive()
    {
        gameObject.SetActive(false);
    }
}