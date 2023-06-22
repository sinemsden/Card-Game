using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attribute
{
    Intellect,
    Physche,
    Physique,
    Motorics,
}

public class CardObject : ScriptableObject
{
    public Attribute attribute;
    public int id;
    public Sprite image;
    public string title;
    public string description;
    public int manaCost;
    public int health;
    public int effectValue;
    public EffectType effectType;
}