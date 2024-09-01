using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShootType {
    Left, Right
}

public class ShootBtn : CustomButton
{
    [SerializeField] float shootCooldown;
    [SerializeField] ShootType shootType;
    [SerializeField] Image mask;
    GameObject player;
    float timer = 0;
    bool isCooldown = false;

    private void Start() {
        player = GameManager.Instance.GetPlayerObj();
    }

    private void Update() {
        if (isCooldown) {
            timer += Time.deltaTime;
            CooldownEffect(timer / shootCooldown);
            if (timer >= shootCooldown) {
                isCooldown = false;
                canClicked = true;
                GetComponent<Button>().transition = Selectable.Transition.ColorTint;
                timer = 0;
                CooldownEffect(1);
            }
        }
    }

    public override void Clicked() {
        base.Clicked();
        if (canClicked) {
            Shoot();
        }
    }

    void Shoot() {
        isCooldown = true;
        canClicked = false;
        GetComponent<Button>().transition = Selectable.Transition.None;
        player.GetComponent<PlayerAttack>().Shoot(shootType);
        CooldownEffect(0);
    }

    void CooldownEffect(float t) {
        mask.fillAmount = 1 - t;
    }
}
