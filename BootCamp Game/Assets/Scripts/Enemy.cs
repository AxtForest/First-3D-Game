using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ GameObject player;
    float health = 100;
    public static int enemyDamage = 10;
    int isRunningHash;
    int isAttackHash;
    bool isAttack;
    bool isRunning;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackHash = Animator.StringToHash("isAttack");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RealizeThePlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Weapon" && PlayerSc.isAttack) 
        {
            TakeDamage();
        }
    }
    void TakeDamage()
    {
        health -= PlayerSc.damage;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void RealizeThePlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <15 && distance>3.5f)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
            isRunning = true;
            HandleAnimation();
        }
        else if(distance <3.5f)
        {
            Attack();
            transform.LookAt(player.transform);
            isRunning = false;
            HandleAnimation();
        }
        
    }
    void HandleAnimation()
    {
        animator.SetBool(isRunningHash, isRunning);
        animator.SetBool(isAttackHash, isAttack);
    }
    void Attack()
    {
        if(!isAttack)
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
