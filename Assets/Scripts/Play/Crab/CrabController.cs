using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfor))]
public class CrabController : MonoBehaviour
{
    [SerializeField] float detectDistance;
    [SerializeField] float moveDistance;
    [SerializeField] float attackDistance;
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform shootTrans;
    [SerializeField] GameObject bubblePrefab;
    int attackDamage;
    float speed;

    EnemyInfor enemyInfor;
    Animator animator;

    Vector3 moveDir;
    float movedDistance;
    bool canMove = false;
    bool isMoving = false;
    bool isAttack = false;
    int sideMove = -1;

    private void Start() {
        enemyInfor = GetComponent<EnemyInfor>();
        animator = GetComponent<Animator>();
        attackDamage = enemyInfor.GetAttackDamage();
        speed = enemyInfor.GetSpeed();
    }

    private void Update() {
        if (enemyInfor.isAlive) {
            if (!canMove) {
                if (Vector3.Distance(transform.position, GameManager.Instance.GetPlayerObj().transform.position) <= detectDistance) {
                    canMove = true;
                }
            }
            if (canMove) {
                if (Vector3.Distance(transform.position, GameManager.Instance.GetPlayerObj().transform.position) > attackDistance && !isMoving) {
                    sideMove *= -1;
                    isMoving = true;
                    Vector3 obj2Player = GameManager.Instance.GetPlayerObj().transform.position - transform.position;
                    obj2Player.y = 0;
                    obj2Player.Normalize();
                    moveDir = Quaternion.Euler(0, sideMove * 40, 0) * obj2Player;
                    movedDistance = 0;
                }
                else if (!isAttack && !isMoving) {
                    animator.SetBool("Attack", true);
                    animator.SetBool("Idle", false);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GameManager.Instance.GetPlayerObj().transform.position - transform.position), rotateSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void FixedUpdate() {
        if (enemyInfor.isAlive) {
            if (isMoving) {
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
                Vector3 currPos = transform.position + speed * moveDir * Time.deltaTime;
                transform.position = currPos;

                Vector3 lookAt = Quaternion.Euler(0, -sideMove * 90, 0) * moveDir;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAt), rotateSpeed * Time.deltaTime);

                movedDistance += speed * Time.deltaTime;
                if (movedDistance >= moveDistance) {
                    movedDistance = 0;
                    isMoving = false;
                }
            }
        }
    }

    public void Attack() {
        isAttack = true;
        GameObject gObj = Instantiate(bubblePrefab, shootTrans.position, Quaternion.identity);
        BubbleBullet bubbleBullet = gObj.GetComponent<BubbleBullet>();
        bubbleBullet.Launch(transform.forward, 10, attackDamage);
    }

    public void StopAttack() {
        isAttack = false;
    }
}
