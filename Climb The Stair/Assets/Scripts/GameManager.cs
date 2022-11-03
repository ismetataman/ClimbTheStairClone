using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    public UIManager uIManager;
    public ParticleSystem sweat;
    public List<GameObject> stairs = new List<GameObject>();
    public Color tiredColor,originalColor;
    public SkinnedMeshRenderer meshRenderer;
    [Header("Player Variables")]
    public float temporaryStamina;
    public float decrementalStamina;
    public float temporaryIncome;
    public float temporaryMoneyIncrease;
    public float temporaryHoldSpeed;
    public float temporaryClickSpeed;
    public float maxMetre = -500f;
    public bool isFinished = false;
    public bool gameStarted = false;
    public bool nextLevel = false;

    private float colorLerpTime = 0.5f;
    private float changer;



    private void Awake()
    {
        MakeSingleton();
    }
    private void Start()
    {
        meshRenderer = GameObject.Find("Player").gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        //Player Data Save
        temporaryStamina = data.maxStamina;
        temporaryIncome = data.incrementalIncome;
        temporaryHoldSpeed = data.incrementalHoldSpeed;
        temporaryClickSpeed = data.incrementalClickSpeed;
        decrementalStamina = data.decrementalStamina;
        temporaryMoneyIncrease = data.moneyIncrease;
    }
    private void Update() 
    {
        GameLose();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StaminaDecrease()
    {
        if (temporaryStamina > 0)
        {
            temporaryStamina -= data.decrementalStamina * Time.deltaTime;
            if (temporaryStamina < 30f)
            {
                TiredPlayerColor();
            }                
        }
        else if (temporaryStamina == 0)
        {
            Debug.Log("Stamina has finished");
        }

    }

    public IEnumerator StaminaInc()
    {
        yield return new WaitForSeconds(1.5f);
        if (temporaryStamina < data.maxStamina)
        {
            temporaryStamina += data.regenerateStamina * Time.deltaTime;
        }
    }

    public void MetreDecremental()
    {
        maxMetre += 0.05f * temporaryHoldSpeed;
    }

    public void TiredPlayerColor()
    {
        meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, tiredColor, colorLerpTime * Time.deltaTime);
        changer = Mathf.Lerp(changer, 1f, colorLerpTime * Time.deltaTime);
        if (changer > 0.9f)
        {
            changer = 0;
        }
    }

    public void GainStaminaback()
    {
        meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, originalColor, colorLerpTime * Time.deltaTime);
        changer = Mathf.Lerp(changer, 1f, colorLerpTime * Time.deltaTime);
        if (changer > 0.9f)
        {
            changer = 0;
        }
    }

    public void MoneyIncrease()
    {
        temporaryIncome += temporaryMoneyIncrease;
    }
    public void GameLose()
    {
        if(temporaryStamina <= 0f)
        {
            isFinished = true;
            temporaryHoldSpeed = 0f;
            temporaryClickSpeed = 0f;
            Debug.Log("YOU LOSE");
        }
    }

}
