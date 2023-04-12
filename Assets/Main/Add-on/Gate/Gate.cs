using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public enum GateType
    {
        Multiplier,
        Adder,
        Substractor,
        Divider
    }
    public GateType type;
    public int gateValue;
    [SerializeField] GameObject blueEffect, redEffect;
    public TextMeshPro gateText;

    private void Start()
    {
        if (type == GateType.Multiplier)
        {
            if (gateValue == 0)
            {   
                gateValue = Random.Range(2, 5);// Rastgele degerler koydum, carpma ise 2 ile 5 aras�nda carps�n mesela.
            }
            gateText.text = "x";
            blueEffect.SetActive(true);
            redEffect.SetActive(false);
        }
        if (type == GateType.Adder)
        {
            if (gateValue == 0)
            {
                gateValue = Random.Range(5, 15);
            }
            gateText.text = "+";
            blueEffect.SetActive(true);
            redEffect.SetActive(false);
        }
        if (type == GateType.Substractor)
        {
            if (gateValue == 0)
            {
                gateValue = Random.Range(5, 15);
            }
            gateText.text = "-";
            blueEffect.SetActive(false);
            redEffect.SetActive(true);
        }
        if (type == GateType.Divider)
        {
            if (gateValue == 0)
            {
                gateValue = Random.Range(2, 5);
            }
            gateText.text = "÷";
            blueEffect.SetActive(false);
            redEffect.SetActive(true);
        }
        gateText.text = gateText.text + gateValue.ToString();
    }
}
