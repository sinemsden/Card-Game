using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effects
{
    public static int GetAttackResult(int value0, int value1)
    {
        return value0 - value1;
    }   
    public static int GetHealResult(int value0, int value1)
    {
        return value0 + value1;
    }   
}