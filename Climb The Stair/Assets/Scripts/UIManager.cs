using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameData data;
    public TextMeshProUGUI staminaLvl, staminaCost, incomeLvl, incomeCost, speedLvl, speedCost, totalMoney;

    public int staminaCount = 1, staminaCostCount = 10;
    public int incomeCount = 1, incomeCostCount = 10;
    public int speedCount = 1, speedCostCount = 10;

    [SerializeField] Image progressBar;
    [SerializeField] GameObject progressBarTarget;
    [SerializeField] GameObject player;
    [SerializeField] GameObject winScreen, loseScreen, incrementals, tapToStart;

    private void Start()
    {
        staminaLvl.text = "LVL" + " " + data.staminaLvl.ToString();
        staminaCost.text = data.staminaCost.ToString();
        incomeLvl.text = "LVL" + " " + data.incomeLvl.ToString();
        incomeCost.text = data.incomeCost.ToString();
        speedLvl.text = "LVL" + " " + data.speedLvl.ToString();
        speedCost.text = data.speedCost.ToString();

    }
    private void Update()
    {
        ProgressionBar();
        GameStarted();
        WinScreen();
        LoseScreen();
        totalMoney.text = GameManager.instance.temporaryIncome.ToString("N0");
    }

    public void StaminaButton()
    {
        if (GameManager.instance.temporaryIncome >= staminaCostCount)
        {
            GameManager.instance.temporaryIncome -= staminaCostCount;
            data.staminaLvl++;
            data.staminaCost += 3;
            data.maxStamina += data.incrementalStamina;
            staminaLvl.text = "LVL" + " " + data.staminaLvl.ToString();
            staminaCost.text = data.staminaCost.ToString();
            Debug.Log("Stamina Purchased");
        }

    }

    public void IncomeButton()
    {
        if (GameManager.instance.temporaryIncome >= incomeCostCount)
        {
            GameManager.instance.temporaryIncome -= incomeCostCount;
            GameManager.instance.temporaryMoneyIncrease += 0.2f;
            data.incomeLvl++;
            data.incomeCost += 2;
            data.moneyIncrease += 0.1f;
            incomeLvl.text = "LVL" + " " + data.incomeLvl.ToString();
            incomeCost.text = data.incomeCost.ToString();
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
            data.speedLvl++;
            data.speedCost += 4;
            data.incrementalHoldSpeed += 0.5f;
            data.incrementalClickSpeed += 0.5f;
            speedLvl.text = "LVL" + " " + data.speedLvl.ToString();
            speedCost.text = data.speedCost.ToString();
            Debug.Log("Speed Purchased");
        }

    }

    public void ProgressionBar()
    {
        progressBar.fillAmount = (player.transform.position.y) / (progressBarTarget.transform.position.y);
    }


    public void WinScreen()
    {
        if (GameManager.instance.maxMetre >= 0)
        {
            GameManager.instance.nextLevel = true;
            winScreen.SetActive(true);
        }
    }
    public void LoseScreen()
    {
        if (GameManager.instance.temporaryStamina <= 0.1f)
        {
            GameManager.instance.sweat.gameObject.SetActive(false);
            loseScreen.SetActive(true);
        }
    }
    public void GameStarted()
    {
        if (GameManager.instance.gameStarted == true)
        {
            tapToStart.SetActive(false);
            incrementals.SetActive(false);
        }
    }
    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.nextLevel = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
