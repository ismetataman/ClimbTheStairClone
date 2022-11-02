using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    public ParticleSystem sweat;
    public List<GameObject> stairs = new List<GameObject>();
    public Color tiredColor;
    public SkinnedMeshRenderer meshRenderer;
    [Header("Player Variables")]
    public float temporaryStamina;
    public float decrementalStamina;
    public float temporaryIncome;
    public float temporaryMoneyIncrease;
    public float temporaryHoldSpeed;
    public float temporaryClickSpeed;
    public float maxMetre = -500f;

    private float colorLerpTime = 0.5f;
    private float changer;



    private void Awake()
    {
        MakeSingleton();
    }
    private void Start()
    {
        meshRenderer = GameObject.Find("Player").gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();


        temporaryStamina = data.maxStamina;
        temporaryIncome = data.incrementalIncome;
        temporaryHoldSpeed = data.incrementalHoldSpeed;
        temporaryClickSpeed = data.incrementalClickSpeed;
        decrementalStamina = data.decrementalStamina;
        temporaryMoneyIncrease = data.moneyIncrease;

        data.maxStamina = 100f;
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
                sweat.gameObject.SetActive(true);
                TiredPlayerColor();
            }
            else
                sweat.gameObject.SetActive(false);   //kontrol et                   
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
        maxMetre += 0.05f;
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

    public IEnumerator MoneyIncrease()
    {
        yield return new WaitForSeconds(0.5f);
        temporaryIncome += temporaryMoneyIncrease;
    }

}
