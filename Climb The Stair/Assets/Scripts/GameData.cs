using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data",menuName = "Game Data")]
public class GameData : ScriptableObject
{
    public float incrementalStamina = 50f;
    public float maxStamina = 100f;
    public float decrementalStamina = 0.5f;
    public float regenerateStamina = 0.1f;
    public float incrementalIncome = 0f;
    public float incrementalHoldSpeed = 1f;
    public float incrementalClickSpeed = 4f;
}
