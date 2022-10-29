using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData data;
    public List<GameObject> stairs = new List<GameObject>();
    [Header("Player Variables")]
    public float temporaryStamina;
    public float decrementalStamina;
    public float temporaryIncome;
    public float temporaryHoldSpeed;
    public float temporaryClickSpeed;
    public float maxMetre = - 500f;


    private void Awake()
    {
        MakeSingleton();
    }
    private void Start()
    {
        temporaryStamina = data.maxStamina;
        temporaryIncome = data.incrementalIncome;
        temporaryHoldSpeed = data.incrementalHoldSpeed;
        temporaryClickSpeed = data.incrementalClickSpeed;
        decrementalStamina = data.decrementalStamina;
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
        if(temporaryStamina > 0)
        {
            temporaryStamina -= data.decrementalStamina;
        }
        else if(temporaryStamina == 0)
        {
            Debug.Log("Stamina Bitti");
        }
        
    }
    public void StaminaIncrease()
    {
        if(temporaryStamina < data.maxStamina )
        {
            temporaryStamina += data.regenerateStamina;
            Debug.Log("Stamina Kazanılıyor");
        }
    }

    public void MetreDecremental()
    {
        maxMetre += 0.05f;
    }
}
