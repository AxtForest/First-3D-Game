using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSc : MonoBehaviour
{
    CharacterController characterController;
    Vector3 moveDirection;
    bool isMoving;
    bool isRunning;
    public static bool isAttack;
    public static float damage = 20;
    int isWalkingHash;
    int isRunningHash;
    int isAttackHash;
    public static int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackHash = Animator.StringToHash("isAttack");

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateWithView();
        Attack();
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();
        //characterController.Move(moveDirection * Time.deltaTime * 5f);
        if (moveDirection.magnitude > 0.1f && !isAttack)
        {
            isMoving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }

            else { isRunning = false; }
        }
        else isMoving = false;
        if(isMoving)
        {
            if(isRunning)
            {
                characterController.Move(moveDirection * Time.deltaTime * 15);
            }
            else
            {
                characterController.Move(moveDirection * Time.deltaTime * 5f);
            }
        }
    }
    void RotateWithView()
    {
        if(moveDirection != Vector3.zero)
        {
            transform.rotation = 
            Quaternion.Slerp
            (transform.rotation, Quaternion.LookRotation(moveDirection), 15f * Time.deltaTime);
            
          
        }
    }
    private void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool(isWalkingHash, isMoving);
        animator.SetBool(isRunningHash, (isRunning && isMoving));
        animator.SetBool(isAttackHash, isAttack);

    }
    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !isAttack)
        {
            isAttack = true;
            StartCoroutine(AttackCoroutine());

        }
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
}
