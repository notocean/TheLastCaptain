using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfor : ObjectInfor 
{
    [SerializeField] bool isBoss;
    [SerializeField] int enemyMaxHealth;
    [SerializeField] int enemyAttackDamage;
    [SerializeField] float enemySpeed;
    [SerializeField] List<Collider> colliders;
    Animator animator;
    [HideInInspector] public bool isAlive = true;

    private void Awake() {
        SetInfor(enemyAttackDamage, enemyMaxHealth, enemySpeed);
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (isAlive && health <= 0) {
            if (isBoss)
                GameManager.Instance.EndGame(true);
            foreach (var collider in colliders) {
                collider.enabled = false;
            }
            animator.SetTrigger("Die");
            isAlive = false;
        }
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
