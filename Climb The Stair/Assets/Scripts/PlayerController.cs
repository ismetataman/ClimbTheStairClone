using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;

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
        PathMovement();
        metreText.text = GameManager.instance.maxMetre.ToString("N1") + "m";
    }

    void PathMovement()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("canRun",true);
            GameManager.instance.MetreDecremental();
            GameManager.instance.StaminaDecrease();
            if (pathCreator != null)
            {
                distanceTravelled += GameManager.instance.temporaryHoldSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            GameManager.instance.MetreDecremental();
            GameManager.instance.StaminaDecrease();
            anim.SetBool("canRun",true);
            if (pathCreator != null)
            {
                distanceTravelled += GameManager.instance.temporaryClickSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
            }
        }
        else
        {
            if(GameManager.instance.temporaryStamina < GameManager.instance.data.maxStamina)
            {
                GameManager.instance.StaminaIncrease();
            }
            anim.SetBool("canRun",false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stair"))
        {
            other.GetComponent<MeshRenderer>().enabled = true;
            GameManager.instance.stairs[stairCounter].GetComponent<MeshRenderer>().enabled = true;
            stairCounter ++;
            metreCounter.transform.position = GameManager.instance.stairs[stairCounter].transform.position + Vector3.up /2;
        }
    }
}