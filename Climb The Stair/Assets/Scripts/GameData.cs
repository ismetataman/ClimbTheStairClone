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
    public float moneyIncrease = 0.2f;
    public float incrementalHoldSpeed = 1f;
    public float incrementalClickSpeed = 4f;

    [Header("Incrementals")]

    public int staminaLvl = 1;
    public int staminaCost = 10;
    public int incomeLvl = 1;
    public int incomeCost = 10;
    public int speedLvl = 1;
    public int speedCost = 10;
}
