using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameData data;
    public TextMeshProUGUI staminaLvl, staminaCost, incomeLvl, incomeCost, speedLvl, speedCost, totalMoney;

    private int staminaCount = 1, staminaCostCount = 10;
    private int incomeCount = 1, incomeCostCount = 10;
    private int speedCount = 1, speedCostCount = 10;

    private void Update()
    {
        totalMoney.text = GameManager.instance.temporaryIncome.ToString("N0");
    }

    public void StaminaButton()
    {
        if (GameManager.instance.temporaryIncome >= staminaCostCount)
        {
            GameManager.instance.temporaryIncome -= staminaCostCount;
            staminaCount++;
            staminaCostCount += 3;
            data.maxStamina += data.incrementalStamina;
            staminaLvl.text = "LVL" + " " + staminaCount.ToString();
            staminaCost.text = staminaCostCount.ToString();
            Debug.Log("Stamina Purchased");
        }

    }

    public void IncomeButton()
    {
        if (GameManager.instance.temporaryIncome >= incomeCostCount)
        {
            GameManager.instance.temporaryIncome -= incomeCostCount;
            GameManager.instance.temporaryMoneyIncrease += 0.2f;
            incomeCount++;
            incomeCostCount += 2;
            incomeLvl.text = "LVL" + " " + incomeCount.ToString();
            incomeCost.text = incomeCostCount.ToString();
            Debug.Log("Income Purchased");
        }

    }
    public void SpeedButton()
    {
        if (GameManager.instance.temporaryIncome >= speedCostCount)
        {
            GameManager.instance.temporaryIncome -= speedCostCount;
            GameManager.instance.temporaryHoldSpeed += 0.2f;
            GameManager.instance.temporaryClickSpeed += 0.2f;
            speedCount++;
            speedCostCount += 4;
            speedLvl.text = "LVL" + " " + speedCount.ToString();
            speedCost.text = speedCostCount.ToString();
            Debug.Log("Speed Purchased");
        }

    }
}
