using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfor))]
public class StarfishController : MonoBehaviour
{
    [SerializeField] float detectDistance;
    [SerializeField] float moveDistance;
    int attackDamage;
    float speed;

    EnemyInfor enemyInfor;
    Animator animator;

    Vector3 moveDir;
    float movedDistance;
    bool canMove = false;
    bool isMoving = false;

    [SerializeField] float delayMoveTime = 1.5f;
    float timer;
    bool isDelay = false;

    private void Start() {
        enemyInfor = GetComponent<EnemyInfor>();
        animator = GetComponent<Animator>();
        attackDamage = GetComponent<EnemyInfor>().GetAttackDamage();
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
                if (!isMoving) {
                    isMoving = true;
                    moveDir = GameManager.Instance.GetPlayerObj().transform.position - transform.position;
                    moveDir.y = 0;
                    moveDir.Normalize();
                    movedDistance = 0;
                }
            }
        }
    }

    private void FixedUpdate() {
        if (enemyInfor.isAlive) {
            if (isMoving) {
                if (isDelay) {
                    timer += Time.deltaTime;
                    if (timer >= delayMoveTime) {
                        isMoving = false;
                        isDelay = false;
                    }
                    animator.SetBool("Attack", false);
                    animator.SetBool("Idle", true);
                }
                else {
                    animator.SetBool("Attack", true);
                    animator.SetBool("Idle", false);
                    Vector3 currPos = transform.position + speed * moveDir * Time.deltaTime;
                    transform.position = currPos;
                    movedDistance += speed * Time.deltaTime;
                    if (movedDistance >= moveDistance) {
                        isDelay = true;
                        movedDistance = 0;
                        timer = 0;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerInfor playerInfor = other.GetComponent<PlayerInfor>();
            playerInfor.TakeDamage(attackDamage);
            AudioManager.Instance.PlayAudio(AudioType.Explosion);
        }
    }
}
