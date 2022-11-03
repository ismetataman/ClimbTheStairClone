using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PathCreator pathCreator;
    public GameObject metreCounter;
    public TextMeshPro metreText;

    public int stairCounter = 0;
    private Rigidbody rb;
    private Animator anim;

    private float distanceTravelled;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        stairCounter = 0;
    }

    private void Update()
    {
        if(!IsPointerOverUIbject())
        {
            PathMovement();
        }
        Sweating();
        metreText.text = GameManager.instance.data.maxMetre.ToString("N1") + "m";
    }

    void PathMovement()
    {
        if (Input.GetMouseButton(0) && GameManager.instance.temporaryStamina > 0 && !GameManager.instance.nextLevel)
        {
            anim.SetBool("canRun", true);
            GameManager.instance.gameStarted = true;
            GameManager.instance.MetreDecremental();
            GameManager.instance.StaminaDecrease();
            if (pathCreator != null)
            {
                distanceTravelled += GameManager.instance.temporaryHoldSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
        else if (Input.GetMouseButtonDown(0) && GameManager.instance.temporaryStamina > 0 && !GameManager.instance.nextLevel)
        {
            GameManager.instance.gameStarted = true;
            GameManager.instance.MetreDecremental();
            GameManager.instance.StaminaDecrease();
            anim.SetBool("canRun", true);
            if (pathCreator != null)
            {
                distanceTravelled += GameManager.instance.temporaryClickSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        }
        else
        {
            if (GameManager.instance.temporaryStamina < GameManager.instance.data.maxStamina && !GameManager.instance.isFinished)
            {
                GameManager.instance.GainStaminaback();
                GameManager.instance.StartCoroutine(GameManager.instance.StaminaInc());
            }
            anim.SetBool("canRun", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stair"))
        {
            GameManager.instance.MoneyIncrease();
            other.GetComponent<MeshRenderer>().enabled = true;
            GameManager.instance.stairs[stairCounter].GetComponent<MeshRenderer>().enabled = true;
            stairCounter++;
            metreCounter.transform.position = GameManager.instance.stairs[stairCounter].transform.position + Vector3.up / 2;
        }
    }

    
     private bool IsPointerOverUIbject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    private void Sweating()
    {
        if (GameManager.instance.temporaryStamina < 30)
        {
            GameManager.instance.sweat.gameObject.SetActive(true);
        }
        else if(GameManager.instance.temporaryStamina >= 30)
        {
            GameManager.instance.sweat.gameObject.SetActive(false);
        }

    }

}
