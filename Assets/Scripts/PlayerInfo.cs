using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
   public int currentHP;
   public int maxHP;
   public Vector2 forestPosition;
   public Vector2 cavePosition;
}
