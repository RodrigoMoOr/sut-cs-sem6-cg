using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    public event Action<Boss> onEncounter;


    float horizontalInput;
    float verticalInput;

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", horizontalInput);
            animator.SetFloat("moveY", verticalInput);
        }
        if (horizontalInput == 0 && verticalInput == 0) animator.SetBool("isMoving", false);

        if (canMove(transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime)))
        {
            transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime);
        }

        CheckForTriggerables();
        

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }



    private bool canMove(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, 0.4f, solidObjectsLayer | interactableLayer))
        {
            return false;
        }
        else return true;
    }


    private void CheckForTriggerables()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f, GameLayers.i.TriggerableLayers);

        foreach (Collider2D collider in colliders)
        {
            IPlayerTriggerable triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.OnTrigger(this);
                break;
            }
        }
    }

    
    void Interact()
    {
        var facingDirection = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDirection;

        Debug.DrawLine(transform.position, interactPosition, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactPosition, 0.4f, GameLayers.i.InteractableLayer);
        if (collider)
        {
            if (collider.gameObject.tag == "Boss")
            {
                Debug.Log("Boss");
                onEncounter(collider.gameObject.GetComponent<Boss>());
                StartCoroutine(collider.gameObject.GetComponent<Interactable>()?.Interact(transform));
            }
            else if (collider.gameObject.tag == "NPC")
            {
                Debug.Log("NPCd");
                StartCoroutine(collider.gameObject.GetComponent<Interactable>()?.Interact(transform));
            }


        }


    }
}