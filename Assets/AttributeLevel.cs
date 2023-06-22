using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeLevel : MonoBehaviour
{
    public static int totalPoint = 8;

    public TextMeshProUGUI text_intellect;
    public TextMeshProUGUI text_physche;
    public TextMeshProUGUI text_physique;
    public TextMeshProUGUI text_motorics;

    public static int intellect = 1;
    public static int physche = 1;
    public static int physique = 1;
    public static int motorics = 1;

    public void SetIntellect(bool increase)
    {
        if (increase == true)
        {
            if (intellect < 3 && totalPoint > 0)
            {
                intellect++;
                text_intellect.text = intellect.ToString();
                totalPoint--;
            }
        }
        else
        {
            if (intellect > 1 && totalPoint <= 8)
            {
                intellect--;
                text_intellect.text = intellect.ToString();
                totalPoint++;
            }
        }
    }
    public void SetPhysche(bool increase)
    {
        if (increase == true)
        {
            if (physche < 3 && totalPoint > 0)
            {
                physche++;
                text_physche.text = physche.ToString();
                totalPoint--;
            }
        }
        else
        {
            if (physche > 1 && totalPoint <= 8)
            {
                physche--;
                text_physche.text = physche.ToString();
                totalPoint++;
            }
        }
    }
    public void SetPhysique(bool increase)
    {
        if (increase == true)
        {
            if (physique < 3 && totalPoint > 0)
            {
                physique++;
                text_physique.text = physique.ToString();
                totalPoint--;
            }
        }
        else
        {
            if (physique > 1 && totalPoint <= 8)
            {
                physique--;
                text_physique.text = physique.ToString();
                totalPoint++;
            }
        }
    }
    public void SetMotorics(bool increase)
    {
        if (increase == true)
        {
            if (motorics < 3 && totalPoint > 0)
            {
                motorics++;
                text_motorics.text = motorics.ToString();
                totalPoint--;
            }
        }
        else
        {
            if (motorics > 1 && totalPoint <= 8)
            {
                motorics--;
                text_motorics.text = motorics.ToString();
                totalPoint++;
            }
        }
    }
}