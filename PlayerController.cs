using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    public InputActionReference movement;
    public bool controlsEnabled;

    private CollectingManager collecting;
    private ConversingManager conversing;
    private DepositManager depositing;
    private UI_InteractMessage interactMessage;

    private Rigidbody playerRB;
    private Animator playerAnim;
    private Vector2 moveDirection2D;
    private Vector3 moveDirection3D;
    private float moveSpeed = 10F; // Would prefer 18F, but it allows it to go through Pond's capsule colliders that high
    private float rotationSpeed = 5F;

    private void Start()
    {
        collecting = FindObjectOfType<CollectingManager>();
        conversing = FindObjectOfType<ConversingManager>();
        depositing = FindObjectOfType<DepositManager>();
        interactMessage = FindObjectOfType<UI_InteractMessage>();

        playerRB = gameObject.GetComponent<Rigidbody>();
        playerAnim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            moveDirection2D = movement.action.ReadValue<Vector2>();
            moveDirection3D = new Vector3(moveDirection2D.x, 0, moveDirection2D.y);
        }
    }

    private void FixedUpdate()
    {
        if (controlsEnabled)
        {
            NavigationProcess();
            RotationProcess();
        }
    }

    private void NavigationProcess()
    {
        if (moveDirection3D != Vector3.zero)
        {
            playerAnim.SetBool("isWalking", true);
            Vector3 newPosition = playerRB.position + moveDirection3D * moveSpeed * Time.deltaTime;
            playerRB.MovePosition(newPosition);
        }
        else
        {
            playerRB.velocity = new Vector3(0, 0, 0);
            playerAnim.SetBool("isWalking", false);
        }
    }

    private void RotationProcess()
    {
        if (moveDirection3D != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection3D);
            playerRB.MoveRotation(Quaternion.Slerp(playerRB.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CollectableBase>())
        {
            collecting.AddCollectable(other.gameObject);
        }
        else if (other.gameObject.GetComponent<ConversableBase>())
        {
            conversing.AddConversable(other.gameObject);
        }
        else if (other.gameObject.GetComponent<DepositManager>())
        {
            depositing.isInteractable = true;
            interactMessage.Observe();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CollectableBase>())
        {
            collecting.RemoveCollectable(other.gameObject);
        }
        else if (other.gameObject.GetComponent<ConversableBase>())
        {
            conversing.RemoveConversable(other.gameObject);
        }
        else if (other.gameObject.GetComponent<DepositManager>())
        {
            depositing.isInteractable = false;
            interactMessage.Observe();
        }
    }
}