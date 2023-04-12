using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : ScriptableObject
{
    public int id;
    public Sprite image;
    public string title;
    public string description;
    public int manaCost;
    public int health;
    public int effectValue;
    public EffectType effectType;
}