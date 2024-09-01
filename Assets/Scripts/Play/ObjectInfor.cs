using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfor : MonoBehaviour, IObjectTakeDamage 
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image healthBar;
    int attackDamage;
    int maxHealth;
    float speed;
    protected int health;

    public void Start() {
        canvas.worldCamera = Camera.main;
    }

    public void SetInfor(int attackDamage, int maxHealth, float speed) {
        this.attackDamage = attackDamage;
        this.maxHealth = maxHealth;
        this.speed = speed;
        health = maxHealth;
    }

    public void TakeDamage(int damage) {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        SetHealthBar();
    }

    private void SetHealthBar() {
        healthBar.fillAmount = (float)health / maxHealth;
    }

    public int GetAttackDamage() {
        return attackDamage;
    }

    public float GetSpeed() {
        return speed;
    }
}
