using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputs playerInputs;
    private InputAction move;
    private InputAction look;
    private InputAction attack;
    private InputAction jump;
    private InputAction sprint;
    private InputAction ESC;

    public GameObject UIPanel;


    public GameObject cameraTarget;
    public CursorLockMode cursorState;
    public float xMouseSensetivity;
    public float yMouseSensetivity;
    private float xRotationVelocity;
    private float yRotationVelocity;
    private float threshold;

    private Animator animator;
    private Rigidbody rb;
    //public bool isGround;
    public float moveSpeed;
    public float sprintMultiplier;
    private bool isSprinting;

    public bool canMove { get; set; }
   // private bool canJump { get; set; }
    private bool canSprint { get; set; }
    private bool canRotate { get; set; }
    public bool canAttack { get; set; }

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        threshold = 0.001f;
        Cursor.lockState = cursorState;

        canMove = true; canSprint = true; canRotate = true; canAttack = true;
    }

    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        move.Enable();

        look = playerInputs.Player.Look;
        look.Enable();
        look.performed += Look;

        attack = playerInputs.Player.Attack;
        attack.Enable();
        attack.started += Attack;

        jump = playerInputs.Player.Jump;
        jump.Enable();

        sprint = playerInputs.Player.Sprint;
        sprint.Enable();
        sprint.performed += SprintPerformed;
        sprint.canceled += SprintEnded;

        ESC = playerInputs.UI.ESC;
        ESC.Enable();
        ESC.started += ESCpressed;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        attack.Disable();
        jump.Disable();
        sprint.Disable();
        ESC.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove) { return; }
        Vector3 moveDirection = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);

        if (isSprinting)
        {
            Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed * sprintMultiplier;
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        }
        else
        {
            Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed;
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        }

        if (moveDirection == Vector3.zero) { animator.SetBool("inMotion", false); }
        else { animator.SetBool("inMotion", true); }
    }

    private void Look(InputAction.CallbackContext context)
    {
        if (!canRotate) { return; }
        Vector2 lookDirection = look.ReadValue<Vector2>();
        if(lookDirection.sqrMagnitude >  threshold)
        {
            xRotationVelocity = lookDirection.x * xMouseSensetivity * Time.deltaTime;
            yRotationVelocity = lookDirection.y * yMouseSensetivity * Time.deltaTime;

            transform.Rotate(Vector3.up * xRotationVelocity);
            cameraTarget.transform.Rotate(Vector3.left * yRotationVelocity);
        }
    }

    private void SprintPerformed(InputAction.CallbackContext context)
    {
        if (!canMove) { return; }
        isSprinting = true;
        animator.SetBool("isSprinting", true);
    }

    private void SprintEnded(InputAction.CallbackContext context)
    {
        isSprinting = false;
        animator.SetBool("isSprinting", false);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (!canAttack) { return; }
            animator.SetTrigger("Attack");
            StartCoroutine(WaitingForAnimAttackEnd());
    }

    private IEnumerator WaitingForAnimAttackEnd()
    {
        canAttack = false;
        canMove = false;
        canRotate = false;
        yield return new WaitForSeconds(1.13f);
        canMove = true;
        canRotate = true;
        canAttack = true;
    }

    private void ESCpressed(InputAction.CallbackContext context)
    {
        if (UIPanel.activeSelf)
        {
            print("lol");
            UIPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }
        else
        {
            UIPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}
