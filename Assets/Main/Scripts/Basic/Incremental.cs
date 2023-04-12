using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using MoreMountains.NiceVibrations;

public class Incremental : MonoBehaviour
{
/*    public Image buttonBackground;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public int level;
    public int maxLevel;
    public float[] costs;
    public float[] effects;
    public string incrementalName;
    public Animation animationComponent;
    public PlayerManager setterComponent;
    public Color canBuyColor;
    public Color cantBuyColor;
    public Incremental[] allIncrementals;
    public void Start()
    {
        if (incrementalName == "arrowDamage")
        {
            level = Mathf.Clamp(PlayerPrefs.GetInt("incremental_arrowDamage"), 1, 1);
            titleText.text = "Damage";
            levelText.text = "Lvl. " + level;
            costText.text = "Cost: " + costs[level - 1];
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "arrowQuantity")
        {
            level = Mathf.Clamp(PlayerPrefs.GetInt("incremental_arrowQuantity"), 1, 1);
            titleText.text = "Quantity";
            levelText.text = "Lvl. " + level;
            costText.text = "Cost: " + costs[level - 1];
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "hp")
        {
            level = Mathf.Clamp(PlayerPrefs.GetInt("incremental_hp"), 1, 1);
            titleText.text = "Health";
            levelText.text = "Lvl. " + level;
            costText.text = "Cost: " + costs[level - 1];
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "sightRange")
        {
            level = Mathf.Clamp(PlayerPrefs.GetInt("incremental_sightRange"), 1, 1);
            titleText.text = "Range";
            levelText.text = "Lvl. " + level;
            costText.text = "Cost: " + costs[level - 1];
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        Set();
        CheckCanBuy();
    }
    public void CheckCanBuy()
    {
        if (GameManager.gold >= costs[level - 1] && level != maxLevel)
        {
            buttonBackground.color = canBuyColor;
            animationComponent.Play("incremental_canBuy");
        }
        else
        {
            buttonBackground.color = cantBuyColor;
            animationComponent.Play("incremental_idle");
        }
    }
    public void Increase()
    {
        CheckCanBuy();
        animationComponent.Play("incremental_buy");
        if (incrementalName == "arrowDamage")
        {
            if (level < maxLevel)
            {
                if (GameManager.gold >= costs[level - 1])
                {
                    GameManager.gold -= (int)costs[level - 1];
                    UIManager.goldText.text = GameManager.gold + "<sprite=0>";
                    level++;
                    levelText.text = "Lvl. " + level;
                    costText.text = "Cost: " + costs[level - 1];
                    PlayerPrefs.SetInt("incremental_arrowDamage", level);
                }
            }
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "arrowQuantity")
        {
            if (level < maxLevel)
            {
                if (GameManager.gold >= costs[level - 1])
                {
                    GameManager.gold -= (int)costs[level - 1];
                    UIManager.goldText.text = GameManager.gold + "<sprite=0>";
                    level++;
                    levelText.text = "Lvl. " + level;
                    costText.text = "Cost: " + costs[level - 1];
                    UIManager.arrowText.text = effects[level - 1] + "/" + effects[level - 1] + "  <sprite=0>";
                    PlayerPrefs.SetInt("incremental_arrowQuantity", level);
                }
            }
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "hp")
        {
            if (level < maxLevel)
            {
                if (GameManager.gold >= costs[level - 1])
                {
                    GameManager.gold -= (int)costs[level - 1];
                    UIManager.goldText.text = GameManager.gold + "<sprite=0>";
                    level++;
                    levelText.text = "Lvl. " + level;
                    costText.text = "Cost: " + costs[level - 1];
                    PlayerPrefs.SetInt("incremental_hp", level);
                    setterComponent.health.healthbar.AddHeart();
                }
            }
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        else if (incrementalName == "sightRange")
        {
            if (level < maxLevel)
            {
                if (GameManager.gold >= costs[level - 1])
                {
                    GameManager.gold -= (int)costs[level - 1];
                    UIManager.goldText.text = GameManager.gold + "<sprite=0>";
                    level++;
                    PlayerPrefs.SetInt("incremental_sightRange", level);
                    levelText.text = "Lvl. " + level;
                    costText.text = "Cost: " + costs[level - 1];
                }
            }
            if (level == maxLevel)
            {
                levelText.text = "Max Lvl.";
                costText.text = "Cost Max";
            }
        }
        foreach (Incremental incremental in allIncrementals)
        {
            incremental.CheckCanBuy();
        }
        MMVibrationManager.Haptic(HapticTypes.RigidImpact);
        Set();
    }
    public void Set()
    {
        if (incrementalName == "arrowDamage")
        {
            setterComponent.damage = effects[level - 1];
            setterComponent.attack.damage = effects[level - 1];
        }
        if (incrementalName == "hp")
        {
            setterComponent.maxHp = effects[level - 1];
            setterComponent.health.maxHealth = effects[level - 1];
            setterComponent.health.health = effects[level - 1];
        }
        if (incrementalName == "arrowQuantity")
        {
            setterComponent.arrowQuantity = effects[level - 1];
        }
        if (incrementalName == "sightRange")
        {
            setterComponent.sightRange = effects[level - 1];
        }
        setterComponent.Set();
    }*/
}