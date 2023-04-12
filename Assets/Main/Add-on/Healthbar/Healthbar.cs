using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] SpriteRenderer healthValueBar;
    void Start()
    {
        if (healthValueBar != null)
        {
            healthValueBar.color = color;
        }
    }
    public void UpdateHealthbar(float characterHealth, float maxCharacterHealth)
    {
        healthValueBar.size = new Vector2(Mathf.Lerp(0, 4.12f, ((characterHealth) / maxCharacterHealth)),0.9f);
    }
}
