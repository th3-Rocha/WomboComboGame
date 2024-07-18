using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool isGrounded = false;
    Animator animator;
    public float teleportCooldown = 0.5f; // Cooldown in seconds
    private float nextTeleportTime = 0f;
    private Vector3 targetPosition;
    public AudioClip TpAudio;
    private AudioSource audioS;
    public GameObject tpEffect;
    public GameObject playerPivotRotate;
    public bool IsAtk;
    private PlayerComboController playerComboController;
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        playerComboController = GetComponent<PlayerComboController>();
    }

    void Update()
    {
        if (Time.time >= nextTeleportTime)
        {
            tpEffect.SetActive(false);

        }
        if (Input.GetMouseButtonDown(1) && Time.time >= nextTeleportTime)
        {
            playerComboController.lastAttackTime = 0;
            StoreMousePosition();
            TeleportPlayerToStoredPosition();
            nextTeleportTime = Time.time + teleportCooldown;
        }
        if (IsAtk)
        {
            rb.useGravity = false;
            animator.Play("atackingPlayer");
        }
        else
        {
            rb.useGravity = true;
        }
     
        animator.SetBool("isGrounded", isGrounded);
    }

    void StoreMousePosition()
    {
        animator.Play("PlayerTeleport"); 
        audioS.PlayOneShot(TpAudio);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }
    }

    void TeleportPlayerToStoredPosition()
    {
        if (targetPosition.x > 0)
        {
            //playerPivotRotate.transform.Rotate(new Vector3(0, -90, 0));
            playerPivotRotate.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else if (targetPosition.x < 0)
        {
            playerPivotRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        rb.MovePosition(targetPosition);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //GameObject instance = Instantiate(tpEffect, gameObject.transform.position, quaternion.identity);
        tpEffect.SetActive(true);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
